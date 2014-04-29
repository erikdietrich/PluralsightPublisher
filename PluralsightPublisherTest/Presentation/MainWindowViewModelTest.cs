using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PluralsightPublisherTest.Presentation
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestClass]
        public class ExitCommand
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Instance_Of_Arbitrary_Command()
            {
                var viewModel = new MainWindowViewModel();

                Assert.IsInstanceOfType(viewModel.ExitCommand, typeof(ArbitraryCommand));
            }
        }
        
    }
}
