using PluralsightPublisher.DataAccess;
using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace PluralsightPublisher.IntegrationTest
{
    public class WorkspaceBuilderTest
    {
        public static class BuildWorkspaceForProject
        {
            private const string WorkingPath = "working";

            private static IProject BuildProject()
            {
                var project = Mock.Create<IProject>();

                project.Arrange(p => p.WorkingDirectory).Returns(WorkingPath);

                return project;
            }

            private static void PerformBuild(IProject project)
            {
                var builder = new BasicWorkspaceBuilder();
                builder.BuildWorkspaceForProject(project);
            }
            public static void Creates_Workspace_Directory()
            {
                var project = BuildProject();

                PerformBuild(project);

                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, project.WorkingDirectory)))
                    throw new IntegrationTestFailedException();
            }

            public static void Deletes_Old_Workspace_Directory()
            {
                var project = BuildProject();

                var di = Directory.CreateDirectory(project.WorkingDirectory);
                var originalCreateTime = di.LastWriteTime;

                PerformBuild(project);

                var updatedCreateTime = Directory.GetCreationTime(project.WorkingDirectory);

                if (updatedCreateTime == originalCreateTime)
                    throw new IntegrationTestFailedException();
            }

            public static void Creates_Module_Subdirectories()
            {
                const string FirstModule = "Module 1";
                const string SecondModule = "Module 2";

                var project = BuildProject();

                project.Arrange(p => p.GetModuleNames()).Returns(new List<string>() { FirstModule, SecondModule });

                PerformBuild(project);

                if (!Directory.Exists(Path.Combine(project.WorkingDirectory, FirstModule)) || !Directory.Exists(Path.Combine(project.WorkingDirectory, SecondModule)))
                    throw new IntegrationTestFailedException();

            }

            public static void Creates_Recordings_Directory_Inside_Module_Directory()
            {
                var project = BuildProject();

                const string moduleName = "Module 1";
                project.Arrange(pr => pr.GetModuleNames()).Returns(new List<String>() { moduleName});

                PerformBuild(project);

                if (!Directory.Exists(Path.Combine(project.WorkingDirectory, moduleName, "Recordings")))
                    throw new IntegrationTestFailedException();
            }

            public static void Creates_Script_Document_In_Module_Directory()
            {
                var project = BuildProject();

                const string moduleName = "Module 1";
                project.Arrange(pr => pr.GetModuleNames()).Returns(new List<String>() { moduleName });

                PerformBuild(project);

                if (!File.Exists(Path.Combine(project.WorkingDirectory, moduleName, "Script.docx")))
                    throw new IntegrationTestFailedException();
            }

            public static void Creates_PowerPoint_Document_In_Module_Directory()
            {
                var project = BuildProject();

                const string moduleName = "Module 1";
                project.Arrange(pr => pr.GetModuleNames()).Returns(new List<String>() { moduleName });

                PerformBuild(project);

                if (!File.Exists(Path.Combine(project.WorkingDirectory, moduleName, "Slides.pptx")))
                    throw new IntegrationTestFailedException();
            }

            public static void Cleanup()
            {
                Directory.Delete(WorkingPath, true);
            }
        }

    }
}
