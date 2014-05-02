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
            Project = new Project() { PublicationDirectory = "asdf", WorkingDirectory = "fdsa" };
            Target = new ProjectViewModel(Project);   
        }

        [TestClass]
        public class WorkingDirectory : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_Underlying_Project_WorkingDirectory()
            {
                var newValue = "Asdfasdfasdf";

                Target.PopulateFromModel(Project);
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

                Target.PopulateFromModel(Project);
                Target.PublicationDirectory = newValue;

                Assert.AreEqual<string>(newValue, Project.PublicationDirectory);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Does_Not_Throw_Exception_When_Underlying_Project_Is_Null()
            {
                var newValue = "123321";

                ExtendedAssert.DoesNotThrow(() => Target.PublicationDirectory = newValue);
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

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_PublicationDirectory_To_Model_PublicationDirectory()
            {
                Target.PopulateFromModel(Project);

                Assert.AreEqual<string>(Project.PublicationDirectory, Target.PublicationDirectory);
            }
        }
    }
}
