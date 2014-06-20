﻿using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;

namespace PluralsightPublisher.Presentation
{
    public class ProjectViewModel : ViewModel
    {
        private readonly IProject _project;
        private readonly IModuleRepository _moduleRepository;
        
        public bool IsValid { get { return _project != null; } }

        public string WorkingDirectory
        {
            get
            {
                return _project != null ? _project.WorkingDirectory : string.Empty;
            }
            set
            {
                if(_project != null)
                    _project.WorkingDirectory = value;

                RaisePropertyChanged();
            }
        }

        public string PublicationDirectory
        {
            get
            {
                return _project != null ? _project.PublicationDirectory : string.Empty;
            }
            set
            {
                if(_project != null)
                    _project.PublicationDirectory = value;

                RaisePropertyChanged();
            }
        }

        public string Title 
        {
            get 
            { 
                return _project != null ? _project.Title : string.Empty; 
            }
            set 
            { 
                if(_project != null)
                    _project.Title = value;

                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IModule> Modules
        {
            get; private set;
        }

        public IProject Project { get { return _project; } }

        public ProjectViewModel(IProject project, IModuleRepository moduleRepository, IEnumerable<IModule> modules = null)
        {
            _moduleRepository = moduleRepository;
            Modules = new ObservableCollection<IModule>(modules ?? Enumerable.Empty<IModule>());
            Modules.CollectionChanged += Modules_CollectionChanged;
            _project = project;
        }

        private void Modules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _moduleRepository.Add(Modules.Last());
        }

    }
}
