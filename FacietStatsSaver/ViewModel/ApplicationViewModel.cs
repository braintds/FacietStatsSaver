using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Input;
using FacietStatsSaver.Infrastructure;
using FacietStatsSaver.Model;
using FacietStatsSaver.Services;
using Newtonsoft.Json;

namespace FacietStatsSaver.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        FacietStatsSaver.Model.faceIt_api _client;

        private readonly IFaceitService _service;       
        public ICommand LoadMatchesCommand { get; }
        
        public MainViewModel(string name)
        {
            _client = new(name);
        }

        public ObservableCollection<Stats> Matches { get; }
            = new ObservableCollection<Stats>();


        public MainViewModel(IFaceitService service)
        {
            _service = service;
            LoadMatchesCommand = new RelayCommand(async _ => await LoadMatches());
        }

        private async Task LoadMatches()
        {
            var matches = await _service.GetMatchesAsync();
            Matches.Clear();

            foreach (var match in matches)
                Matches.Add(match);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<string?> checkAccAsync(string name) => (await _client.getAccountAsync(name, CancellationToken.None)).account.player_id;
        
        public async Task<List<Model.Stats>> LastMatchesAsync(DateTime from, DateTime to, decimal countMatches, decimal startPosition) => await _client.getPlayerMatchesAsync(from, to, 5, 0, CancellationToken.None);
    }
}
