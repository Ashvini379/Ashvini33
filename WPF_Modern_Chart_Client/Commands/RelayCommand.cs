using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Modern_Chart_Client.Commands
{
    public class RelayCommand : ICommand
    {
        Action _handler;

        public RelayCommand(Action h)
        {
            _handler = h;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
