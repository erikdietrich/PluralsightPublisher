using PluralsightPublisher.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Presentation
{
    public class ProjectViewModel : ViewModel
    {
        private readonly Project _project;
        private readonly IEnumerable<Module> _modules;
        
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

        public IEnumerable<string> ModuleNames
        {
            get
            {
                return _modules.Select(m => m.Name);
            }
        }
        public Project Project { get { return _project; } }

        public ProjectViewModel(Project project, IEnumerable<Module> modules = null)
        {
            _modules = modules ?? Enumerable.Empty<Module>();
            _project = project;
        }

    }
}
