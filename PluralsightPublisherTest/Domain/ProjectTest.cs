using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisherTest.Domain
{
    [TestClass]
    public class ProjectTest
    {
        [TestClass]
        public class ClearModules
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_When_Cleared_On_Empty()
            {
                var project = new Project();

                ExtendedAssert.DoesNotThrow(() => project.ClearModules());
            }
        }
    }
}
