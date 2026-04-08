using System.Windows.Input;

namespace FacietStatsSaver.Infrastructure
{
    class RelayCommand : ICommand
    {
        //private readonly Func<object?, Task> _execute;
        //private readonly Predicate<object?>? _canExecute;

        //public RelayCommand(Func<object?, Task> execute,
        //                    Predicate<object?>? canExecute = null)
        //{
        //    _execute = execute;
        //    _canExecute = canExecute;
        //}
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        public RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute?.Invoke() ?? true;

        public async void Execute(object? parameter)
            => await _execute();
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler? CanExecuteChanged;
    }
}
