using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using FacietStatsSaver.Infrastructure;
using FacietStatsSaver.Model;
using FacietStatsSaver.Services;
using Newtonsoft.Json;

namespace FacietStatsSaver.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFaceitService _service;
        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadMatchesCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<Stats> Matches { get; } = new();

        public ICommand ValidateAccountCommand { get; }
        //public MainViewModel(IFaceitService service)
        //{
        //    _service = service;
        //    LoadMatchesCommand = new RelayCommand(async _ => await LoadMatches());
        //}
        public MainViewModel(IFaceitService faceitService)
        {
            _service = faceitService;

            ValidateAccountCommand = new RelayCommand(ValidateAccountAsync);
        }
        private async Task LoadMatches(DateTime from, DateTime to, decimal countMatches, decimal startPosition)
        {
            var matches = await _service.GetMatchesAsync(from,to, countMatches, startPosition);
            Matches.Clear();

            foreach (var match in matches)
                Matches.Add(match);
        }

        private async Task ValidateAccountAsync()
        {
            var result = await _service.getAccountAsync(Nickname, CancellationToken.None); //Обработка токена будет добавлена позже

            if (string.IsNullOrEmpty(result.account.player_id))
            {
                MessageBox.Show("Неверный никнейм");
            }
            else
            {
                MessageBox.Show("Аккаунт найден");
            }
        }

        public async Task<string?> checkAccAsync(string name) => (await _service.getAccountAsync(name, CancellationToken.None)).account.player_id;

        //public async Task<List<Model.Stats>> LastMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition) => await _client.getPlayerMatchesAsync(from, to, 5, 0, CancellationToken.None);
    }
}
