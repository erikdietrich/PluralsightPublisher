using PluralsightPublisher.Domain;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IXmlDocument _document;
        private readonly DomainRoot _domainRoot;

        
        public ModuleRepository(IXmlDocument document, DomainRoot domainRoot)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;
            _domainRoot = domainRoot;
        }

        public IEnumerable<IModule> GetAllForProject(string projectId)
        {
            var xml = _document.Load(projectId);
            var modules = xml.Descendants("Module").Select(node =>
                new Module()
                {
                    Name = node.Attribute("Name").Value
                });

            foreach (var module in modules)
                _domainRoot.GetRoot().AddModule(module);

            return modules;
        }

        public void SetModules(params IModule[] modules)
        {
            var root = _domainRoot.GetRoot();
            root.ClearModules();

            foreach(var module in modules)
                root.AddModule(new Module(module));

        }
    }
}
