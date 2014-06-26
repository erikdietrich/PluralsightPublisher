using PluralsightPublisher.Types;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PluralsightPublisher.DataAccess
{

    public class BasicWorkspaceBuilder : IWorkspaceBuilder
    {
        public void BuildWorkspaceForProject(IProject project)
        {
            CreateProjectDirectory(project);

            foreach (var moduleName in project.GetModuleNames())
                BuildModuleDirectoryStructure(Path.Combine(project.WorkingDirectory, moduleName));
        }

        private static void CreateProjectDirectory(IProject project)
        {
            if (Directory.Exists(project.WorkingDirectory))
                Directory.Delete(project.WorkingDirectory, true);

            Directory.CreateDirectory(project.WorkingDirectory);
        }

        private static void BuildModuleDirectoryStructure(string moduleDirectory)
        {
            Directory.CreateDirectory(moduleDirectory);
            Directory.CreateDirectory(Path.Combine(moduleDirectory, "Recordings"));

            var document = new Document();
            var paragraph = document.AddSection().AddParagraph();
            paragraph.AppendText("This is a Pluralsight module script");
            document.SaveToFile(Path.Combine(moduleDirectory, "Script.docx"));

            File.Copy(Path.Combine("Deliverables", "PluralsightSlideTemplate.pptx"), Path.Combine(moduleDirectory, "Slides.pptx"));
        }
    }
}
