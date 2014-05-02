using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PluralsightPublisher.Types;
using PluralsightPublisher.DataTransfer;
using System.ComponentModel;

namespace PluralsightPublisher.Presentation
{
    public class MainWindowViewModel : ViewModel 
    {
        private readonly IRepository<Project> _repository;

        private readonly ArbitraryCommand _exitCommand = new ArbitraryCommand(() => Application.Current.Shutdown());
        public ICommand ExitCommand { get { return _exitCommand; } }

        private readonly ArbitraryCommand _saveCommand;
        public ArbitraryCommand SaveCommand { get { return _saveCommand; } }

        public ProjectViewModel ProjectViewModel { get; private set; }

        public MainWindowViewModel(IRepository<Project> repository)
        {
            if(repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
            ProjectViewModel = new ProjectViewModel();

            _saveCommand = new ArbitraryCommand(SaveProject, (o) => ProjectViewModel.IsValid);
        }

        public void CreateNewProject(string projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var projectToCreate = new Project() { ProjectPath = projectPath };

            _repository.Create(projectToCreate);
            UpdateProjectViewModel(projectToCreate);
        }

        public void LoadProject(string projectPath)
        {
            if(string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var project = _repository.GetById(projectPath);
            UpdateProjectViewModel(project);
        }

        public void SaveProject()
        {
            if(!ProjectViewModel.IsValid)
                throw new InvalidOperationException("Cannot save without loading a project.");

            _repository.Update(ProjectViewModel.Project);
        }

        private void UpdateProjectViewModel(Project project)
        {
            ProjectViewModel.PopulateFromModel(project);
            RaisePropertyChanged("ProjectViewModel");
        }

    }
}
