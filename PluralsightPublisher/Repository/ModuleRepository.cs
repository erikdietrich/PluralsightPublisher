using PluralsightPublisher.DataTransfer;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Module> GetAllForProject(string projectId)
        {
            var xml = _document.Load(projectId);
            return xml.Descendants("Module").Select(node =>
                new Module()
                {
                    Name = node.Attribute("Name").Value
                });
        }
    }
}
