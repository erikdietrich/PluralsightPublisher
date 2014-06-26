using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.IntegrationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkspaceBuilderTest.BuildWorkspaceForProject.Creates_Workspace_Directory();
            WorkspaceBuilderTest.BuildWorkspaceForProject.Deletes_Old_Workspace_Directory();
            WorkspaceBuilderTest.BuildWorkspaceForProject.Creates_Module_Subdirectories();
            WorkspaceBuilderTest.BuildWorkspaceForProject.Creates_Recordings_Directory_Inside_Module_Directory();
            WorkspaceBuilderTest.BuildWorkspaceForProject.Creates_Script_Document_In_Module_Directory();
            WorkspaceBuilderTest.BuildWorkspaceForProject.Creates_PowerPoint_Document_In_Module_Directory();

            WorkspaceBuilderTest.BuildWorkspaceForProject.Cleanup();
        }
    }
}
