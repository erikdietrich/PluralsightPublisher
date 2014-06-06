using PluralsightPublisher.Types.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IProjectRepository
    {
        IProject GetById(string id);

        void Save(IProject itemToCreate);
    }
}
