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

        public ModuleRepository(IXmlDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;
        }

        public IEnumerable<IModule> GetAllForProject(string projectId)
        {
            var xml = _document.Load(projectId);
            return xml.Descendants("Module").Select(node =>
                new Module()
                {
                    Name = node.Attribute("Name").Value
                });
        }

        public void Save(IModule module)
        {
            
        }
    }
}
