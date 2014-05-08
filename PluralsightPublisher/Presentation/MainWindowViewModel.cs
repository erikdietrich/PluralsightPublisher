﻿using System;
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
        private readonly IProjectRepository<Project> _repository;

        private readonly ArbitraryCommand _exitCommand = new ArbitraryCommand(() => Application.Current.Shutdown());
        public ICommand ExitCommand { get { return _exitCommand; } }

        private readonly ArbitraryCommand _saveCommand;
        public ArbitraryCommand SaveCommand { get { return _saveCommand; } }

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


        public MainWindowViewModel(IProjectRepository<Project> repository)
        {
            if(repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;

            ProjectViewModel = new ProjectViewModel(null);
            _saveCommand = new ArbitraryCommand(SaveProject, (o) => ProjectViewModel.IsValid);
        }

        public void CreateNewProject(string projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var projectToCreate = new Project() { ProjectPath = projectPath };

            _repository.Save(projectToCreate);
            ProjectViewModel = new ProjectViewModel(projectToCreate);
        }

        public void LoadProject(string projectPath)
        {
            if(string.IsNullOrEmpty(projectPath))
                throw new ArgumentException("projectPath");

            var project = _repository.GetById(projectPath);
            ProjectViewModel = new ProjectViewModel(project);
        }

        public void SaveProject()
        {
            if(!ProjectViewModel.IsValid)
                throw new InvalidOperationException("Cannot save without loading a project.");

            _repository.Save(ProjectViewModel.Project);
            StatusMessage = "Project saved.";
        }

    }
}
