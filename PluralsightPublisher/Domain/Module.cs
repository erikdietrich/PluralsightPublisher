using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Domain
{
    public class Module : IModule
    {
        public string Name { get; set; }

        public Module(IModule moduleToCopy = null)
        {
            if(moduleToCopy != null)
                Name = moduleToCopy.Name;
        }
    }
}
