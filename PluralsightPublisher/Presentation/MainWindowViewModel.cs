using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PluralsightPublisher.Types;
using PluralsightPublisher.Domain;

namespace PluralsightPublisher.Presentation
{
    public class MainWindowViewModel : ViewModel 
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IModuleRepository _moduleRepository;

        private readonly ArbitraryCommand _exitCommand = new ArbitraryCommand(() => Application.Current.Shutdown());
        public ICommand ExitCommand { get { return _exitCommand; } }

        private readonly ArbitraryCommand _saveCommand;
        public ArbitraryCommand SaveCommand { get { return _saveCommand; } }

        private ArbitraryCommand _createWorkingCommand;
        public ICommand CreateWorkingCommand { get { return _createWorkingCommand; } }

        private ProjectViewModel _projectViewModel;
        public ProjectViewModel ProjectViewModel
        {
            get
            {
                return _projectViewModel;
            }
            private set
            {
                _projectViewModel = value;
                RaisePropertyChanged();
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            private set
            {
                _statusMessage = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowViewModel(IProjectRepository projectRepository, IModuleRepository moduleRepository)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("repository");

            _moduleRepository = moduleRepository;
            _projectRepository = projectRepository;

            ProjectViewModel = new ProjectViewModel(null);

            _saveCommand = new ArbitraryCommand(SaveProject, (o) => ProjectViewModel.IsValid);
            _createWorkingCommand = new ArbitraryCommand(() => _projectRepository.BuildWorkspace(ProjectViewModel.Project));
        }

        public void CreateNewProject(string projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var projectToCreate = new Project() { ProjectPath = projectPath };

            _projectRepository.Save(projectToCreate);
            ProjectViewModel = new ProjectViewModel(projectToCreate);
        }

        public void LoadProject(string projectPath)
        {
            if(string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var project = _projectRepository.GetById(projectPath);
            var modules = _moduleRepository.GetAllForProject(projectPath);

            ProjectViewModel = new ProjectViewModel(project, modules);
        }

        public void SaveProject()
        {
            if(!ProjectViewModel.IsValid)
                throw new InvalidOperationException("Cannot save without loading a project.");

            _projectRepository.Save(ProjectViewModel.Project);
            StatusMessage = "Project saved.";
        }

    }
}
