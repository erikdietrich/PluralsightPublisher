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
        public Project GetById(string id)
        {
            var xml = XDocument.Load(id);
            return new Project()
            {
                WorkingDirectory = xml.Descendants("WorkingDirectory").First().Value,
                PublicationDirectory = xml.Descendants("PublicationDirectory").First().Value,
                ProjectPath = id
            };
        }

        public void Update(Project itemToUpdate)
        {
            var root = new XElement("Project");
            root.Add(new XElement("WorkingDirectory", itemToUpdate.WorkingDirectory));
            root.Add(new XElement("PublicationDirectory", itemToUpdate.PublicationDirectory));
            root.Save(itemToUpdate.ProjectPath);
        }

        public void Create(Project projectToCreate)
        {
            var root = new XElement("Project");
            root.Add(new XElement("WorkingDirectory", string.Empty));
            root.Add(new XElement("PublicationDirectory", string.Empty));
            root.Save(projectToCreate.ProjectPath);
        }
    }
}
