using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Domain;
using PluralsightPublisher.Presentation;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace PluralsightPublisherTest.Presentation
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        private IProjectRepository ProjectRepository { get; set; }
        private IModuleRepository ModuleRepository { get; set; }


        private MainWindowViewModel Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            ProjectRepository = Mock.Create<IProjectRepository>();
            ModuleRepository = Mock.Create<IModuleRepository>();

            Target = new MainWindowViewModel(ProjectRepository, ModuleRepository);
        }

        [TestClass]
        public class ExitCommand : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Instance_Of_Arbitrary_Command()
            {
                Assert.IsInstanceOfType(Target.ExitCommand, typeof(ArbitraryCommand));
            }
        }

        [TestClass]
        public class SaveCommand : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void CannotExecute_When_Project_Has_Not_Loaded()
            {
                Assert.IsFalse(Target.SaveCommand.CanExecute(null));
            }
        }

        [TestClass]
        public class CreateWorkingCommand : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Repository_BuildWorkspace_When_Executed()
            {
                Target.CreateWorkingCommand.Execute(null);

                ProjectRepository.Assert(pr => pr.BuildWorkspace(Arg.IsAny<IProject>()), Occurs.Once());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Cannot_Execute_When_Project_Has_Not_Loaded()
            {
                Assert.IsFalse(Target.CreateWorkingCommand.CanExecute(null));
            }

        }

        [TestClass]
        public class Constructor : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_On_Null_Argument()
            {
                ExtendedAssert.Throws<ArgumentNullException>(() => new MainWindowViewModel(null, null));
            }
        }

        [TestClass]
        public class CreateNewProject : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Repository_Create_With_Project_Whose_Path_Reflects_Passed_In_Value()
            {
                const string filePath = "asdf";

                Target.CreateNewProject(filePath);

                ProjectRepository.Assert(r => r.Save(Arg.Matches<Project>(p => p.ProjectPath == filePath)), Occurs.Once());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_ArgumentException_On_Null_Path()
            {
                ExtendedAssert.Throws<ArgumentException>(() => Target.CreateNewProject(null));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Results_In_Valid_Project()
            {
                Target.CreateNewProject("asdf");

                Assert.IsTrue(Target.ProjectViewModel.IsValid);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_Property_Changed_On_ProjectViewModel()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled = true;

                Target.CreateNewProject("fdas");

                Assert.IsTrue(wasCalled);
            }
        }

        [TestClass]
        public class LoadProject : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Populates_Project_With_Values_Retrieved_From_Repository()
            {
                const string workingDirectory = "asdf";
                var projectFromRepository = new Project() { WorkingDirectory = workingDirectory };
                ProjectRepository.Arrange(p => p.GetById(Arg.AnyString)).Returns(projectFromRepository);

                Target.LoadProject("whatever");

                Assert.AreEqual<string>(projectFromRepository.WorkingDirectory, Target.ProjectViewModel.WorkingDirectory);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_ArgumentException_On_Empty_Path()
            {
                ExtendedAssert.Throws<ArgumentException>(() => Target.LoadProject(string.Empty));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_PropertyChanged_For_ProjectViewModel()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled = true;

                Target.LoadProject("fdas");

                Assert.IsTrue(wasCalled);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Populates_ProjectViewModel_ModuleNames_With_Values_From_Repository()
            {
                const string moduleName = "fdsa";
                ModuleRepository.Arrange(mr => mr.GetAllForProject(Arg.AnyString)).Returns(new List<Module>() { new Module() { Name = moduleName } });

                Target.LoadProject("34234li");

                Assert.AreEqual<string>(moduleName, Target.ProjectViewModel.Modules.First().Name);
            }
        }

        [TestClass]
        public class SaveProject : MainWindowViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Invokes_Repository_Update_Method()
            {
                var theProject = new Project() { WorkingDirectory = "fdsa" };
                ProjectRepository.Arrange(pr => pr.GetById(Arg.AnyString)).Returns(theProject);

                Target.LoadProject("asdf");
                Target.SaveProject();

                ProjectRepository.Assert(pr => pr.Save(theProject), Occurs.Once());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_When_Project_Has_Not_Been_Loaded()
            {
                ExtendedAssert.Throws<InvalidOperationException>(() => Target.SaveProject());
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Sets_StatusText_To_Save_Successful()
            {
                Target.LoadProject("asdf");
                Target.SaveProject();

                Assert.AreEqual<string>("Project saved.", Target.StatusMessage);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_PropertyChanged_For_StatusMessage()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled |= e.PropertyName == "StatusMessage";

                Target.LoadProject("fdsa");
                Target.SaveProject();

                Assert.IsTrue(wasCalled);
            }
        }
    }
}
