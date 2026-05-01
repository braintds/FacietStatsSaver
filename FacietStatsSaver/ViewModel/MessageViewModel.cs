using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using FacietStatsSaver.Infrastructure;

namespace FacietStatsSaver.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        
        public MessageViewModel(string Message) {
            _message = Message;
        }
        
        public Action CloseAction { get; set; }

        private string _message = "Сообщение";
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand => new RelayCommand(() =>
        {
            CloseAction?.Invoke();

            return Task.CompletedTask;
        });
    }
}
