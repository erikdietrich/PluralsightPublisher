using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace PluralsightPublisher.Domain
{
    public class Project : IProject
    {
        private readonly IList<Module> _modules;

        public string ProjectPath { get; set; }
        public string WorkingDirectory { get; set; }
        public string PublicationDirectory { get; set; }
        public string Title { get; set; }

        public Project() : this(new List<Module>())
        { }

        public Project(IList<Module> modules)
        {
            _modules = modules;
        }

        public void AddModule(Module moduleToAdd)
        {
            _modules.Add(moduleToAdd);
        }

        public IEnumerable<string> GetModuleNames()
        {
            return _modules.Select(m => m.Name);
        }

    }
}
