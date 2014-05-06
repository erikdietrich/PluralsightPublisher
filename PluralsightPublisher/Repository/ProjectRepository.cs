using PluralsightPublisher.DataTransfer;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PluralsightPublisher.Repository
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly IXmlDocument _document;

        public ProjectRepository(IXmlDocument document)
        {
            if(document == null)
                throw new ArgumentNullException("document");

            _document = document;
        }

        public Project GetById(string id)
        {
            var xml = _document.Load(id);
            return new Project()
            {
                WorkingDirectory = GetDescendantValueOrNull(xml, "WorkingDirectory"),
                PublicationDirectory = GetDescendantValueOrNull(xml, "PublicationDirectory"),
                Title = GetDescendantValueOrNull(xml, "Title"),
                ProjectPath = id
            };
        }

        public void Save(Project itemToUpdate)
        {
            if(itemToUpdate == null)
                throw new ArgumentNullException("itemToUpdate");

            var root = new XElement("Project");
            root.Add(new XElement("WorkingDirectory", itemToUpdate.WorkingDirectory ?? string.Empty));
            root.Add(new XElement("PublicationDirectory", itemToUpdate.PublicationDirectory ?? string.Empty));
            root.Add(new XElement("Title", itemToUpdate.Title ?? string.Empty));
            _document.Save(root, itemToUpdate.ProjectPath);
        }

        private static string GetDescendantValueOrNull(XDocument document, XName nodeName)
        {
            var node = document.Descendants(nodeName).FirstOrDefault();
            return node != null ? node.Value : null;
        }


    }
}
