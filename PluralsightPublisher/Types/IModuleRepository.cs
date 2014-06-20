using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IModuleRepository
    {
        void Save(IModule module);

        IEnumerable<IModule> GetAllForProject(string projectId);

        void Add(IModule moduleToAdd);
    }
}
