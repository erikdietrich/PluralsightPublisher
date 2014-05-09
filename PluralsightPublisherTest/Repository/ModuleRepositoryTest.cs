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

namespace PluralsightPublisherTest.Repository
{
    [TestClass]
    public class ModuleRepositoryTest
    {
        [TestClass]
        public class GetAll
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Empty_Collection_When_No_Matches_Are_Found()
            {
                var document = Mock.Create<IXmlDocument>();

                var repository = new ModuleRepository();

                Assert.AreEqual<int>(0, repository.GetAll().Count());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_One_Module_When_One_Module_Exists()
            {
                var document = Mock.Create<IXmlDocument>();

            }
        }
    }

    public class ModuleRepository : IModuleRepository
    {
        public IEnumerable<Module> GetAll()
        {
            return Enumerable.Empty<Module>();
        }
    }
}
