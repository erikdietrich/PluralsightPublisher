using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.Types
{
    public interface IFilesystem
    {
        void WipeAndCreateDirectory(string path);

        void CreateDirectory(string path);
    }
}
