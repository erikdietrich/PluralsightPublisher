using PluralsightPublisher.Types.DataTransfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PluralsightPublisher.Presentation
{
    public class ProjectViewModel : ViewModel
    {
        private readonly IProject _project;
        
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

        public ProjectViewModel(IProject project, IEnumerable<IModule> modules = null)
        {
            Modules = new ObservableCollection<IModule>(modules ?? Enumerable.Empty<IModule>());
            _project = project;
        }

    }
}
