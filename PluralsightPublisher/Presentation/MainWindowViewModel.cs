using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PluralsightPublisher.Presentation
{
    public class MainWindowViewModel
    {
        private readonly ArbitraryCommand _exitCommand = new ArbitraryCommand(() => Application.Current.Shutdown());
        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand;
            }
        }

        public string ProjectPath { get; set; }

    }
}
