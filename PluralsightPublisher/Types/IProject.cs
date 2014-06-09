using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Types
{
    public interface IProject
    {
        string ProjectPath { get; set; }
        string WorkingDirectory { get; set; }
        string PublicationDirectory { get; set; }
        string Title { get; set; }

        IEnumerable<string> GetModuleNames();
    }
}
