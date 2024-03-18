using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _Execute { get; }
        private Predicate<object> _CanExecute { get; }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod)
        {
            _Execute = ExecuteMethod ?? throw new ArgumentNullException(nameof(ExecuteMethod));
            _CanExecute = CanExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested += value;
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
