using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Presentation;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;

namespace PluralsightPublisherTest.Presentation
{
    [TestClass]
    public class ModuleModelTest
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_Name_To_Name_Of_Passed_In_IModule()
            {
                var module = Mock.Create<IModule>();
                module.Name = "asdfasfda";
     
                var moduleModel = new ModuleModel(module);

                Assert.AreEqual<string>(module.Name, moduleModel.Name);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_Null_When_Nothing_Is_Passed_In()
            {
                var moduleModel = new ModuleModel();

                Assert.IsNull(moduleModel.Name);
            }
        }
        
    }

}
