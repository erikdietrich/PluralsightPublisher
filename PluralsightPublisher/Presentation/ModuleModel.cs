using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Presentation
{
    public class ModuleModel : ViewModel, IModule 
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public ModuleModel() : this(null) { }

        public ModuleModel(IModule module)
        {
            if (module != null)
                Name = module.Name;
        }
    }
}
