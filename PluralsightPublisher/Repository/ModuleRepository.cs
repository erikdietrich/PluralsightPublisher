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

        public void Add(object param1)
        {
            throw new NotImplementedException();
        }
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

        public void Save(IModule module)
        {
            
        }

        public void Add(IModule module)
        {

        }
    }
}
