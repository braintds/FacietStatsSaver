using System.Windows.Input;

namespace FacietStatsSaver.Infrastructure
{
    class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<Task> _update;
        private readonly Func<bool> _canExecute;
        private Func<DateTime, DateTime, decimal, decimal, Task> lastMatchesAsync;

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
