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

using FacietStatsSaver.Services;
using Newtonsoft.Json;

namespace FacietStatsSaver.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFaceitService _service;

        private bool _realTimeStats = false;
        public bool RealTimeStats
        {
            get => _realTimeStats;
            set
            {
                _realTimeStats = value;
                OnPropertyChanged();

                realTimeStatsIsOn();
            }
        }

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
        private string _AVGKills = "N/A";
        public string AVGKills
        {
            get => _AVGKills;
            set
            {
                _AVGKills = value;
                OnPropertyChanged();
            }
        }

        private string _hsPercentage = "N/A";
        public string HsPercentage
        {
            get => _hsPercentage;
            set
            {
                _hsPercentage = value;
                OnPropertyChanged();
            }
        }

        private string _wlRatio = "N/A";
        public string WLRatio
        {
            get => _wlRatio;
            set
            {
                _wlRatio = value;
                OnPropertyChanged();
            }
        }

        private bool _isAccountValid = false;
        public bool IsAccountValid
        {
            get => _isAccountValid;
            set
            {
                _isAccountValid = value;
                OnPropertyChanged();
            }
        }

        private string _avgKd = "N/A";
        public string AvgKd
        {
            get => _avgKd;
            set
            {
                _avgKd = value;
                OnPropertyChanged();
            }
        }
        private DateTime _fromDate { get; set; } = DateTime.Now.AddDays(-7);
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _toDate { get; set; } = DateTime.Now;
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged();
            }
        }

        public decimal CountMatches { get; set; } = 100;
        public decimal StartPosition { get; set; } = 0;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<Domain.Stats> Matches { get; } = new();

        public ICommand ValidateAccountCommand { get; }
        public ICommand LastMatchesCommand { get; }
        public ICommand PreviewStatsCommand { get; }
        public ICommand CalculateRealTimeStatsCommand { get; }
        //public MainViewModel(IFaceitService service)
        //{
        //    _service = service;
        //    LoadMatchesCommand = new RelayCommand(async _ => await LoadMatches());
        //}
        public MainViewModel(IFaceitService faceitService)
        {
            _service = faceitService;
            

            ValidateAccountCommand = new RelayCommand(ValidateAccountAsync);
            LastMatchesCommand = new RelayCommand(LastMatchesAsync);
            CalculateRealTimeStatsCommand = new RelayCommand(CalculateRealTimeStats);
            PreviewStatsCommand = new RelayCommand(PreviewStats);
        }
        /*private async Task LoadMatches(DateTime from, DateTime to, decimal countMatches, decimal startPosition)
        {
            var matches = await _service.GetMatchesAsync(from, to, countMatches, startPosition);
            Matches.Clear();

            foreach (var match in matches)
                Matches.Add(match);
        }*/

        private async Task ValidateAccountAsync()
        {
            var result = await _service.getAccountAsync(Nickname, CancellationToken.None); //Обработка токена будет добавлена позже

            if (string.IsNullOrEmpty(result.account.player_id))
            {
                MessageBox.Show("Неверный никнейм");
                IsAccountValid = false;
            }
            else
            {
                MessageBox.Show("Аккаунт найден");
                IsAccountValid = true;
            }
        }


        private async Task LastMatchesAsync()
        {
            try
            {
                Matches.Clear();
                var result = await _service.GetMatchesAsync(FromDate, ToDate, CountMatches, StartPosition, CancellationToken.None);
                if (result.Count == 0)
                {
                    MessageBox.Show("Матчи не найдены");
                    return;
                }
                else
                {

                    foreach (var match in result)
                        Matches.Add(Infrastructure.Mapper.StatsMapper.ToDomain(match));

                    
                    int wins = Matches.Count(x => x.Result == "Win");
                   
                    WLRatio = $"{wins}/{Matches.Count - wins}";
                    AVGKills = (Matches.Sum(x=>x.Kills)/Matches.Count).ToString();
                    HsPercentage = (Matches.Sum(x=>x.HeadshotsPercentage)/Matches.Count).ToString();
                    AvgKd = (Matches.Sum(x => x.KDRatio) / Matches.Count).ToString("F2");
                }
            }
            catch (ArgumentException ex) { 
                MessageBox.Show($"{ex.Message}", "ERROR");
                
            }
            

        }
        public async Task<string?> checkAccAsync(string name) => (await _service.getAccountAsync(name, CancellationToken.None)).account.player_id;

        public async Task CalculateRealTimeStats()
        {
            ToDate = DateTime.UtcNow;
            
            await LastMatchesAsync();
            IsAccountValid = true;
        }

        public async Task PreviewStats()
        {
            ToDate = DateTime.UtcNow;
            await LastMatchesAsync();
        }

        public void realTimeStatsIsOn() 
        {
            FromDate = DateTime.UtcNow;
            IsAccountValid = false;
        }
        
        public void SaveStats()
        {
            if (Matches.Count < 0)
            {
                MessageBox.Show($"Nothing to save!");
            }
            else
            {
                MessageBox.Show($"Coming soon");
            }
        }
    }
}
