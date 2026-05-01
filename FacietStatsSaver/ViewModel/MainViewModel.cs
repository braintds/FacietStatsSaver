using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
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
        private readonly IFaceitService _faceitService;
        private readonly IStatisticsService _statisticsService;

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
        public MainViewModel(IFaceitService faceitService, IStatisticsService statisticsService)
        {
            _faceitService = faceitService;
            _statisticsService = statisticsService;

            ValidateAccountCommand = new RelayCommand(ValidateAccountAsync);
            LastMatchesCommand = new RelayCommand(LastMatchesAsync);
            CalculateRealTimeStatsCommand = new RelayCommand(CalculateRealTimeStats);
            PreviewStatsCommand = new RelayCommand(PreviewStats);
        }

        private async Task ValidateAccountAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Nickname))
                    throw new ArgumentNullException("Nickname is null!");

                var result = await _faceitService.getAccountAsync(Nickname, CancellationToken.None); //Обработка токена будет добавлена позже

                if (string.IsNullOrEmpty(result.account.player_id))
                {
                    var MessageBox = new MessageWindow("Nickname is incorrect");
                    MessageBox.ShowDialog();
                    IsAccountValid = false;
                }
                else
                {
                    var MessageBox = new MessageWindow($"Account find");
                    MessageBox.ShowDialog();
                    IsAccountValid = true;
                }
            }
            catch (HttpRequestException ex)
            {
                var MessageBox = new MessageWindow(ex.StatusCode.ToString());
                MessageBox.ShowDialog();
            }
            catch (ArgumentNullException ex) 
            {
                var MessageBox = new MessageWindow(ex.ParamName != null ? ex.ParamName : ex.Message);
                MessageBox.ShowDialog();
            }

        }


        private async Task LastMatchesAsync()
        {
            try
            {
                Matches.Clear();
                var result = await _faceitService.GetMatchesAsync(FromDate, ToDate, CountMatches, StartPosition, CancellationToken.None);
                if (result.Count == 0)
                {
                    var MessageBox = new MessageWindow("Матчи не найдены");
                    MessageBox.ShowDialog(); 
                    return;
                }
                else
                {

                    foreach (var match in result)
                        Matches.Add(Infrastructure.Mapper.StatsMapper.ToDomain(match));

                   
                    WLRatio = $"{_statisticsService.CalcutateWins(Matches)}/{_statisticsService.CalcutateLoses(Matches)}";
                    AVGKills = _statisticsService.CalculateAVG(Matches).ToString("F0");
                    HsPercentage = _statisticsService.CalculateHS(Matches).ToString("F0");
                    AvgKd = _statisticsService.CalculateKD(Matches.ToList()).ToString("F2");
                }
            }
            catch (ArgumentException ex) { 
                var CustomMessageBox = new MessageWindow($"{ex.Message}");
                CustomMessageBox.ShowDialog();
            }
            

        }

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
            if (Matches.Count == 0)
            {
                var CustomMessageBox = new MessageWindow($"Nothing to save!");
                CustomMessageBox.ShowDialog();
            }
            else
            {
                var CustomMessageBox = new MessageWindow($"Coming soon");
                CustomMessageBox.ShowDialog();
            }
        }
    }
}
