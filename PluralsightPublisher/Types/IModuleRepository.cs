using PluralsightPublisher.Types.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IModuleRepository
    {
        void Save(Module module);
        IEnumerable<IModule> GetAllForProject(string projectId);
    }
}
