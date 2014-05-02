using PluralsightPublisher.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            }
        }

        public Project Project { get { return _project; } }

        public void PopulateFromModel(Project projectModelToUse)
        {
            _project = projectModelToUse;
        }

        

    }
}
