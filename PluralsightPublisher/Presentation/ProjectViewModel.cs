using PluralsightPublisher.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Presentation
{
    public class ProjectViewModel : ViewModel
    {
        private Project _project;

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

        public Project Project { get { return _project; } }

        public ProjectViewModel(Project project)
        {
            _project = project;
        }

    }
}
