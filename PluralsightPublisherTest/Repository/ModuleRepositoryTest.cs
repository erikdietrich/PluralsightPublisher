using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using System.Xml.Linq;
using PluralsightPublisher.Repository;
using PluralsightPublisher.Domain;

namespace PluralsightPublisherTest.Repository
{
    [TestClass]
    public class ModuleRepositoryTest
    {
        private const string NormalProjectText = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Project><WorkingDirectory>asdf</WorkingDirectory><PublicationDirectory>fdsa</PublicationDirectory><Title>My Project</Title><Module Name=\"Module 1\"/></Project>";

        private IXmlDocument Document { get; set; }
        private DomainRoot DomainRoot { get; set; }

        private ModuleRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Document = Mock.Create<IXmlDocument>();
            Document.Arrange(doc => doc.Load(Arg.AnyString)).Returns(XDocument.Parse(NormalProjectText));

            DomainRoot = Mock.Create<DomainRoot>();
            DomainRoot.Arrange(dr => dr.GetRoot()).Returns(new Project());

            Target = new ModuleRepository(Document, DomainRoot);
        }

        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ModuleRepository(null, null)); 
            }
        }

        [TestClass]
        public class GetAllForProject : ModuleRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Empty_Collection_When_No_Matches_Are_Found()
            {
                Document.Arrange(doc => doc.Load(Arg.AnyString)).Returns(new XDocument());
                Assert.AreEqual<int>(0, Target.GetAllForProject("whatevs").Count());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_One_Module_When_One_Module_Exists()
            {
                Assert.AreEqual<int>(1, Target.GetAllForProject("blah").Count());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Module_With_Name_Corresponding_To_Node_Name_Attribute()
            {
                var module = Target.GetAllForProject("blah").First();

                Assert.AreEqual<string>("Module 1", module.Name);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Adds_New_Module_To_In_Memory_Project()
            {
                var project = new Project();
                DomainRoot.Arrange(dr => dr.GetRoot()).Returns(project);

                var module = Target.GetAllForProject("asdf").First();

                Assert.AreEqual<int>(1, project.GetModuleNames().Count());
            }
        }

        [TestClass]
        public class Save : ModuleRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_XDoc_Save()
            {
                
            }
        }

        [TestClass]
        public class SetModules : ModuleRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Adds_Modules_Passed_In_To_DomainRoot()
            {
                var project = new Project();
                DomainRoot.Arrange(dr => dr.GetRoot()).Returns(project);

                Target.SetModules(Mock.Create<IModule>(), Mock.Create<IModule>());

                Assert.AreEqual<int>(2, project.GetModuleNames().Count());
            }

        }
    }
}
