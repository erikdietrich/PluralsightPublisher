using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Domain;
using PluralsightPublisher.Repository;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace PluralsightPublisherTest.Repository
{
    [TestClass]
    public class ProjectRepositoryTest
    {
        private readonly static string ValidDocument = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Project><WorkingDirectory>C:\\Users\\edietrich\\Desktop</WorkingDirectory><PublicationDirectory>C:\\Users\\edietrich</PublicationDirectory><Title>My Project</Title><Module Name=\"Module 1\"/><Module Name=\"Module 2\"/></Project>";

        private IXmlDocument XmlDocument { get; set; }

        private IFilesystem Filesystem { get; set; }

        private ProjectRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            XmlDocument = Mock.Create<IXmlDocument>();
            Filesystem = Mock.Create<IFilesystem>();

            Target = new ProjectRepository(XmlDocument, Filesystem);
        }

        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ProjectRepository(null, null));
            }
        }

        [TestClass]
        public class GetById : ProjectRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Document_Load_With_Passed_In_Path()
            {
                const string path = "asdf";

                Target.GetById(path);

                XmlDocument.Assert(rep => rep.Load(path), Occurs.Once());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Project_With_Title_Matching_File()
            {
                XmlDocument.Arrange(x => x.Load(Arg.AnyString)).Returns(XDocument.Parse(ValidDocument));

                var project = Target.GetById("asdf");

                Assert.AreEqual<string>("My Project", project.Title);
            }
        }

        [TestClass]
        public class Save : ProjectRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Document_Save_On_Project_Path()
            {
                const string path = "aasdfasdfa";

                Target.Save(new Project() { ProjectPath = path });

                XmlDocument.Assert(d => d.Save(Arg.IsAny<XElement>(), path), Occurs.Once());

            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => Target.Save(null));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Saves_With_A_Node_For_Title()
            {
                const string theTitle = "asdf";

                Target.Save(new Project() { Title = theTitle });

                XmlDocument.Assert(d => d.Save(Arg.Matches<XElement>(xe => xe.Descendants().Any(x => x.Value == theTitle)), Arg.AnyString), Occurs.Once());
            }
        }

        [TestClass]
        public class BuildWorkspace : ProjectRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Filesystem_WipeDirectoryAndMakeNew()
            {
                const string path = "fas";

                Target.BuildWorkspace(new Project() { WorkingDirectory = path });

                Filesystem.Assert(fs => fs.WipeAndCreateDirectory(path), Occurs.Once());

            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => Target.BuildWorkspace(null));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_CreateDirectory_Three_Times_For_Project_With_Three_Modules()
            {
                const int count = 3;
                var project = new Project(new Module() { Name = "asdf" }.AsList(count)) { WorkingDirectory = "Fdsa" };

                Target.BuildWorkspace(project);

                Filesystem.Assert(fs => fs.CreateDirectory(Arg.AnyString), Occurs.Exactly(count));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_CreateDirectory_With_Project_WorkingDirectory_And_Module_Name_Combined()
            {
                const string moduleName = "asdf";
                const string projectPath = "fasdfasdf";

                var module = new Module() { Name = moduleName };
                var project = new Project(module.AsList()) { WorkingDirectory = projectPath };

                Target.BuildWorkspace(project);

                Filesystem.Assert(fs => fs.CreateDirectory(Path.Combine(projectPath, moduleName)), Occurs.Once());
            }
        }
    }
}
