using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IWorkspaceBuilder
    {
        void BuildWorkspaceForProject(IProject project);
    }
}
