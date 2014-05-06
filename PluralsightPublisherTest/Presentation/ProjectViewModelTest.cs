using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.DataTransfer;
using PluralsightPublisher.Presentation;

namespace PluralsightPublisherTest.Presentation
{
    [TestClass]
    public class ProjectViewModelTest
    {
        private ProjectViewModel Target { get; set; }

        private Project Project { get; set; } 

        [TestInitialize]
        public void BeforeEachTest()
        {
            Project = new Project() { PublicationDirectory = "asdf", WorkingDirectory = "fdsa", Title = "Some Project" };
            Target = new ProjectViewModel(Project);   
        }

        [TestClass]
        public class WorkingDirectory : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_Underlying_Project_WorkingDirectory()
            {
                var newValue = "Asdfasdfasdf";

                Target.WorkingDirectory = newValue;

                Assert.AreEqual<string>(newValue, Project.WorkingDirectory);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_When_Underlying_Project_Is_Null()
            {
                var newValue = "ddddd";

                ExtendedAssert.DoesNotThrow(() => Target.WorkingDirectory = newValue);
            }
        }

        [TestClass]
        public class PublicationDirectory : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_Underlying_Project_WorkingDirectory()
            {
                var newValue = "fffff";

                Target.PublicationDirectory = newValue;

                Assert.AreEqual<string>(newValue, Project.PublicationDirectory);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_When_Underlying_Project_Is_Null()
            {
                var newValue = "123321";

                ExtendedAssert.DoesNotThrow(() => Target.PublicationDirectory = newValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Null_When_Internal_Project_Is_Null()
            {
                var viewModel = new ProjectViewModel(null);

                Assert.AreEqual<string>(string.Empty, viewModel.PublicationDirectory);
            }
        }

        [TestClass]
        public class Title : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_Title_On_Project()
            {
                Assert.AreEqual<string>(Project.Title, Target.Title);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_Title_On_Internal_Project_To_Value()
            {
                const string newTitle = "fdsa";

                Target.Title = newTitle;

                Assert.AreEqual<string>(newTitle, Project.Title);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Null_When_Internal_Project_Is_Null()
            {
                var viewModel = new ProjectViewModel(null);

                Assert.AreEqual<string>(string.Empty, viewModel.Title);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_PropertyChanged()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled |= e.PropertyName == "Title";

                Target.Title = "fdsa";

                Assert.IsTrue(wasCalled);
            }
        }

        [TestClass]
        public class Constructor : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_Working_Directory_Of_Passed_In_Project()
            {
                Assert.AreEqual<string>(Project.WorkingDirectory, Target.WorkingDirectory);
            }
        }
    }
}
