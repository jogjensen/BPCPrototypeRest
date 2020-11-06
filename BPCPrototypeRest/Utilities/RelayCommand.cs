using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPCPrototypeRest.Utilities
{
    public class RelayCommand
    {

        private Predicate<object> _predicate;
        private Action _method;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action method, Predicate<object> pred)
        {
            _method = method; //trigger
            _predicate = pred; //can trigger?
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null ? true : _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _method();
        }
    }
}

