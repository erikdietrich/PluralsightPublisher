using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPublisher.Types.DataTransfer
{
    public interface IProject
    {
        string ProjectPath { get; set; }
        string WorkingDirectory { get; set; }
        string PublicationDirectory { get; set; }
        string Title { get; set; }
    }
}
