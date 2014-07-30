using PluralsightPublisher.Domain;
using PluralsightPublisher.Types;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PluralsightPublisher.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IWorkspaceBuilder _workspaceBuilder;
        private readonly IXmlDocument _document;
        private readonly DomainRoot _domainRoot;

        public ProjectRepository(IXmlDocument document, IWorkspaceBuilder workspaceBuilder, DomainRoot domainRoot)
        {
            VerifyPreconditionsOrThrow(document, workspaceBuilder, domainRoot);

            _document = document;
            _workspaceBuilder = workspaceBuilder;
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

            if(_domainRoot.GetRoot() == null)
                _domainRoot.SetRoot(new Project(itemToUpdate));
            else
                _domainRoot.GetRoot().CopyProjectPropertiesFrom(itemToUpdate);

            var root = _domainRoot.GetRoot();

            var xmlRoot = BuildXmlStructureForProject(root);
            _document.Save(xmlRoot, root.ProjectPath);
        }

        private static XElement BuildXmlStructureForProject(IProject itemToUpdate)
        {
            var root = new XElement("Project");
            root.Add(new XElement("WorkingDirectory", itemToUpdate.WorkingDirectory ?? string.Empty));
            root.Add(new XElement("PublicationDirectory", itemToUpdate.PublicationDirectory ?? string.Empty));
            root.Add(new XElement("Title", itemToUpdate.Title ?? string.Empty));
            
            foreach (var moduleElement in BuildElementsForProject(itemToUpdate))
                root.Add(moduleElement);

            return root;
        }

        private static IEnumerable<XElement> BuildElementsForProject(IProject project)
        {
            foreach (var name in project.GetModuleNames())
            {
                var moduleNode = new XElement("Module");
                moduleNode.SetAttributeValue("Name", name);
                yield return moduleNode;
            }
        }

        public void BuildWorkspace(IProject projectToBuildOut)
        {
            if (projectToBuildOut == null)
                throw new ArgumentNullException("projectToBuildOut");

            var project = _domainRoot.GetRoot();
            _workspaceBuilder.BuildWorkspaceForProject(project);
        }

        private static void VerifyPreconditionsOrThrow(IXmlDocument document, IWorkspaceBuilder workspaceBuilder, DomainRoot domainRoot)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (workspaceBuilder == null)
                throw new ArgumentNullException("workspaceBuilder");
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

        private static string GetDescendantValueOrNull(XDocument document, XName nodeName)
        {
            var node = document.Descendants(nodeName).FirstOrDefault();
            return node != null ? node.Value : null;
        }


    }
}
