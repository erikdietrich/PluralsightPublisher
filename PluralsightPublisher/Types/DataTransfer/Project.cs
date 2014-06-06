using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.Types.DataTransfer
{
    public class Project : IProject
    {
        public string ProjectPath { get; set; }
        public string WorkingDirectory { get; set; }
        public string PublicationDirectory { get; set; }
        public string Title { get; set; }
    }
}
