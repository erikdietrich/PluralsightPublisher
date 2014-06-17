using PluralsightPublisher.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.DataAccess
{
    public class BasicFilesystem : IFilesystem
    {
        public void WipeAndCreateDirectory(string path)
        {
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
