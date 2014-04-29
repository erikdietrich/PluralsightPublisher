using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PluralsightPublisher.Presentation
{
    public class ArbitraryCommand : ICommand
    {
        private readonly Action _action;

        public ArbitraryCommand(Action action)
        {
            if(action == null)
                throw new ArgumentNullException("action");

            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
