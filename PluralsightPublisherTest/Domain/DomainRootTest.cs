using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisherTest.Domain
{
    [TestClass]
    public class DomainRootTest
    {
        [TestClass]
        public class GetRoot
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Most_Recently_Set_Project_As_Root()
            {
                var root = new DomainRoot();
                var project = new Project();

                root.SetRoot(project);

                Assert.AreEqual<Project>(project, root.GetRoot());
            }
        }
    }
}
