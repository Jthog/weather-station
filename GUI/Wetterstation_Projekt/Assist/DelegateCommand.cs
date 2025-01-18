using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wetterstation_Projekt.Assist
{
    public class DelegateCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Predicate<object> canExecute, Action<object> execute) => (this.canExecute, this.execute) = (canExecute, execute);
        public DelegateCommand(Action<object> execute) : this(null, execute) { }

        public void RaisedCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => this.execute?.Invoke(parameter);
    }
}
