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
        private IWorkspaceBuilder WorkspaceBuilder { get; set; }
        private DomainRoot DomainRoot { get; set; }

        private ProjectRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            XmlDocument = Mock.Create<IXmlDocument>();
            WorkspaceBuilder = Mock.Create<IWorkspaceBuilder>();
            DomainRoot = Mock.Create<DomainRoot>();

            DomainRoot.Arrange(dr => dr.GetRoot()).Returns((Project)null);
            DomainRoot.Arrange(dr => dr.SetRoot(Arg.IsAny<Project>())).DoInstead((Project p) => DomainRoot.Arrange(dr => dr.GetRoot()).Returns(p));

            Target = new ProjectRepository(XmlDocument, WorkspaceBuilder, DomainRoot);
        }

        [TestClass]
        public class Constructor : ProjectRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ProjectRepository(null, WorkspaceBuilder, DomainRoot));
                ExtendedAssert.Throws<ArgumentNullException>(() => new ProjectRepository(XmlDocument, null, DomainRoot));
                ExtendedAssert.Throws<ArgumentNullException>(() => new ProjectRepository(XmlDocument, WorkspaceBuilder, null));
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

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_DomainRoot_To_Point_At_Retrieved_Object()
            {
                XmlDocument.Arrange(x => x.Load(Arg.AnyString)).Returns(XDocument.Parse(ValidDocument));

                Target.GetById("fdsa");

                DomainRoot.Assert(dr => dr.SetRoot(Arg.IsAny<Project>()), Occurs.Once());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Root_From_DomainRoot_When_It_Exists()
            {
                const string projectTitle = "blah";
                DomainRoot.Arrange(dr => dr.GetRoot()).Returns(new Project() { Title = projectTitle });

                var project = Target.GetById("whatevs");

                Assert.AreEqual<string>(projectTitle, project.Title);
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
            public void Invokes_WorkspaceBuilder_On_Project()
            {

                var project = new Project();

                DomainRoot.Arrange(dr => dr.GetRoot()).Returns(project);

                Target.BuildWorkspace(new Project());

                WorkspaceBuilder.Assert(wsb => wsb.BuildWorkspaceForProject(project), Occurs.Once());

            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => Target.BuildWorkspace(null));
            }

        }
    }
}
