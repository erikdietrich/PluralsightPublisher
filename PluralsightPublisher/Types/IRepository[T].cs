using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluralsightPublisher.DataTransfer;

namespace PluralsightPublisher.Types
{
    public interface IRepository<T>
    {
        T GetById(string id);
        void Create(T itemToCreate);
        void Update(T itemToUpdate);
    }
}
