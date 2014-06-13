using PluralsightPublisher.Domain;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PluralsightPublisher.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IFilesystem _filesystem;
        private readonly IXmlDocument _document;
        private readonly DomainRoot _domainRoot;

        public ProjectRepository(IXmlDocument document, IFilesystem filesystem, DomainRoot domainRoot)
        {
            VerifyPreconditionsOrThrow(document, filesystem, domainRoot);

            _document = document;
            _filesystem = filesystem;
            _domainRoot = domainRoot;
        }

        public IProject GetById(string id)
        {
            var inMemoryProject = _domainRoot.GetRoot();

            if (inMemoryProject == null)
                LoadProjectFromDiskById(id);

            return _domainRoot.GetRoot();
        }

        public void Save(IProject itemToUpdate)
        {
            if (itemToUpdate == null)
                throw new ArgumentNullException("itemToUpdate");

            var root = new XElement("Project");
            root.Add(new XElement("WorkingDirectory", itemToUpdate.WorkingDirectory ?? string.Empty));
            root.Add(new XElement("PublicationDirectory", itemToUpdate.PublicationDirectory ?? string.Empty));
            root.Add(new XElement("Title", itemToUpdate.Title ?? string.Empty));
            _document.Save(root, itemToUpdate.ProjectPath);
        }

        public void BuildWorkspace(IProject projectToBuildOut)
        {
            if (projectToBuildOut == null)
                throw new ArgumentNullException("projectToBuildOut");

            var project = _domainRoot.GetRoot();

            _filesystem.WipeAndCreateDirectory(project.WorkingDirectory);

            CreateModuleDirectories(_domainRoot.GetRoot());
        }

        private static void VerifyPreconditionsOrThrow(IXmlDocument document, IFilesystem filesystem, DomainRoot domainRoot)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (filesystem == null)
                throw new ArgumentNullException("filesystem");
            if (domainRoot == null)
                throw new ArgumentNullException("domainRoot");
        }

        private void LoadProjectFromDiskById(string id)
        {
            var xml = _document.Load(id);
            var project = new Project()
            {
                WorkingDirectory = GetDescendantValueOrNull(xml, "WorkingDirectory"),
                PublicationDirectory = GetDescendantValueOrNull(xml, "PublicationDirectory"),
                Title = GetDescendantValueOrNull(xml, "Title"),
                ProjectPath = id
            };
            _domainRoot.SetRoot(project);
        }

        private void CreateModuleDirectories(IProject projectToBuildOut)
        {
            foreach (var moduleName in projectToBuildOut.GetModuleNames())
            {
                var moduleRootDirectoryPath = Path.Combine(projectToBuildOut.WorkingDirectory, moduleName);
                _filesystem.CreateDirectory(moduleRootDirectoryPath);
            }
        }

        private static string GetDescendantValueOrNull(XDocument document, XName nodeName)
        {
            var node = document.Descendants(nodeName).FirstOrDefault();
            return node != null ? node.Value : null;
        }


    }
}
