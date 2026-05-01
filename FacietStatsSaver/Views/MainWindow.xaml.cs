using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FacietStatsSaver.Model;
using FacietStatsSaver.Services;
using Newtonsoft.Json;
using FacietStatsSaver.ViewModel;

namespace FacietStatsSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var api = new faceIt_api();
            DataContext = new MainViewModel(new FaceitService(api), new StatisticsService());
        }
    }
}