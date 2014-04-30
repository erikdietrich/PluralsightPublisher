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
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ArbitraryCommand(Action action) : this(action, null) { }

        public ArbitraryCommand(Action action, Func<object, bool> canExecute = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _action = action;

            _canExecute = canExecute ?? (o => true);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }
        

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
