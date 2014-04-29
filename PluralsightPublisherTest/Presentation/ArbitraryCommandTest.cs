using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisherTest.Presentation
{

    [TestClass]
    public class ArbitraryCommandTest
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new ArbitraryCommand(null));
            }
        }
        [TestClass]
        public class Execute
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Triggers_Execution_Of_Action_Passed_To_Constructor()
            {
                var wasCalled = false;

                var command = new ArbitraryCommand(() => wasCalled = true);
                command.Execute(null);

                Assert.IsTrue(wasCalled);
            }
        }

        [TestClass]
        public class CanExecuteChanged
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_On_Add()
            {
                ExtendedAssert.DoesNotThrow(() => new ArbitraryCommand(() => { }).CanExecuteChanged += (o, e) => { });
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_On_Remove()
            {
                ExtendedAssert.DoesNotThrow(() => new ArbitraryCommand(() => { }).CanExecuteChanged -= (o, e) => { });
            }
        }

        [TestClass]
        public class CanExecute
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_True()
            {
                var command = new ArbitraryCommand(() => { });

                Assert.IsTrue(command.CanExecute(null));
            }
        }
    }
}
