﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PluralsightPublisher.Domain;
using PluralsightPublisher.Presentation;
using PluralsightPublisher.Types;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace PluralsightPublisherTest.Presentation
{
    [TestClass]
    public class ProjectViewModelTest
    {
        private ProjectViewModel Target { get; set; }

        private IModuleRepository ModuleRepository { get; set; }
        private IProject Project { get; set; }
        private IEnumerable<IModule> Modules { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            ModuleRepository = Mock.Create<IModuleRepository>();

            Project = new Project() { PublicationDirectory = "asdf", WorkingDirectory = "fdsa", Title = "Some Project" };
            Modules = new List<Module>();
            Target = new ProjectViewModel(Project, ModuleRepository, Modules);   
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

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_PropertyChanged()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled |= e.PropertyName == "WorkingDirectory";

                Target.WorkingDirectory = "fda";

                Assert.IsTrue(wasCalled);
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
                var viewModel = new ProjectViewModel(null, ModuleRepository);

                Assert.AreEqual<string>(string.Empty, viewModel.PublicationDirectory);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Raises_PropertyChanged()
            {
                var wasCalled = false;
                Target.PropertyChanged += (o, e) => wasCalled |= e.PropertyName == "PublicationDirectory";

                Target.PublicationDirectory = "fda";

                Assert.IsTrue(wasCalled);
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
                var viewModel = new ProjectViewModel(null, ModuleRepository);

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
        public class ModulesProperty : ProjectViewModelTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Instance_Of_ModuleModel()
            {
                var module = Mock.Create<IModule>();
                var modules = new List<IModule>() { module };

                Target = new ProjectViewModel(Project, ModuleRepository, modules);

                Assert.IsInstanceOfType(Target.Modules.First(), typeof(ModuleModel));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Passed_In_Module()
            {
                const string moduleName = "fdsa";
                Modules = new List<Module>() { new Module() { Name = moduleName } };
                Target = new ProjectViewModel(Project, ModuleRepository, Modules);

                Assert.AreEqual<string>(moduleName, Target.Modules.First().Name);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Empty_When_Nothing_Is_Passed_In()
            {
                Target = new ProjectViewModel(Project, ModuleRepository);

                Assert.AreEqual<int>(0, Target.Modules.Count());
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
