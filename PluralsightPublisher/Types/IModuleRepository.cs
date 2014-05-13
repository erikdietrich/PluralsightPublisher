using PluralsightPublisher.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.Types
{
    public interface IModuleRepository
    {
        void Save(Module module);
        IEnumerable<Module> GetAllForProject(string projectId);
    }
}
