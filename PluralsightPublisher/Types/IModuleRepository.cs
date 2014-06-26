using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IModuleRepository
    {
        IEnumerable<IModule> GetAllForProject(string projectId);

        void SetModules(params IModule[] moduleToAdd);
    }
}
