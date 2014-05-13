using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.DataTransfer;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using System.Xml.Linq;
using PluralsightPublisher.Repository;

namespace PluralsightPublisherTest.Repository
{
    [TestClass]
    public class ModuleRepositoryTest
    {
        private const string NormalProjectText = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Project><WorkingDirectory>asdf</WorkingDirectory><PublicationDirectory>fdsa</PublicationDirectory><Title>My Project</Title><Module Name=\"Module 1\"/></Project>";

        private IXmlDocument Document { get; set; } 

        private ModuleRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Document = Mock.Create<IXmlDocument>();
            Target = new ModuleRepository(Document);
        }

        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ModuleRepository(null)); 
            }
        }

        [TestClass]
        public class GetAllForProject : ModuleRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Empty_Collection_When_No_Matches_Are_Found()
            {
                Assert.AreEqual<int>(0, Target.GetAllForProject("whatevs").Count());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_One_Module_When_One_Module_Exists()
            {
                Document.Arrange(doc => doc.Load(Arg.AnyString)).Returns(XDocument.Parse(NormalProjectText));

                Assert.AreEqual<int>(1, Target.GetAllForProject("blah").Count());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Module_With_Name_Corresponding_To_Node_Name_Attribute()
            {
                Document.Arrange(doc => doc.Load(Arg.AnyString)).Returns(XDocument.Parse(NormalProjectText));

                var name = Target.GetAllForProject("blah").First().Name;

                Assert.AreEqual<string>("Module 1", name);
            }
        }
    }
}
