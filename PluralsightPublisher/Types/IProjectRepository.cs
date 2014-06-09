using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IProjectRepository
    {
        void BuildWorkspace(IProject projectToBuildOut);
        IProject GetById(string id);

        void Save(IProject itemToCreate);
    }
}
