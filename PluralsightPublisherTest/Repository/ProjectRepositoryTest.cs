using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Repository;
using PluralsightPublisher.Types;
using PluralsightPublisher.Types.DataTransfer;
using System;
using System.Collections.Generic;
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

        private ProjectRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            XmlDocument = Mock.Create<IXmlDocument>();

            Target = new ProjectRepository(XmlDocument);
        }

        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_NullArgumentException_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ProjectRepository(null));
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
    }
}
