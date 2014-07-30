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

        public Project(IProject project)
        {
            CopyProjectPropertiesFrom(project);
            _modules = project.GetModuleNames().Select(m => new Module() { Name = m }).ToList();
        }

        public void CopyProjectPropertiesFrom(IProject projectToCopy)
        {
            ProjectPath = projectToCopy.ProjectPath;
            WorkingDirectory = projectToCopy.WorkingDirectory;
            PublicationDirectory = projectToCopy.PublicationDirectory;
            Title = projectToCopy.Title;
        }

        public void AddModule(Module moduleToAdd)
        {
            _modules.Add(moduleToAdd);
        }

        public void ClearModules()
        {
            _modules.Clear();
        }

        public IEnumerable<string> GetModuleNames()
        {
            return _modules.Select(m => m.Name);
        }
    }
}
