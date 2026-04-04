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
using Newtonsoft.Json;

namespace FacietStatsSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FacietStatsSaver.ViewModel.ApplicationViewModel? _model = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _model = new(this.link_str.Text);
            if (!string.IsNullOrEmpty(await _model.checkAccAsync(this.link_str.Text)))
            {
                this.accountFindResult.Text = "account found successfully";
                this.accountFindResult.Visibility = Visibility.Visible;
                this.accountFindResult.Foreground = Brushes.Green;
                this.matchesButton.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var debugWnd = new debugWnd();

            
            string response = null;
            if (this.FromDatePicker != null)
            {
                if (this.ToDatePicker != null)
                {//привязка https://ru.stackoverflow.com/questions/937239/%D0%94%D0%BE%D0%B1%D0%B0%D0%B2%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5-%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85-%D0%B2-datagrid-%D0%B2-wpf
                    response = await _model.LastMatchesAsync(this.FromDatePicker.SelectedDate.Value, this.ToDatePicker.SelectedDate.Value, 5, 0);
                    MainDataGrid.ItemsSource = (JsonConvert.DeserializeObject<getPlayerMatchesResponse>(response)).matches.items;

                    return;
                }
                response = await _model.LastMatchesAsync(this.FromDatePicker.SelectedDate.Value, DateTime.UtcNow, 5, 0);
                MainDataGrid.ItemsSource = (JsonConvert.DeserializeObject<getPlayerMatchesResponse>(response)).matches.items;
                return;
            }
            else
            {
                var msg = MessageBox.Show("Choose date in box!");
            }
            debugWnd.Show();
            debugWnd.ShowViewModel(response == null ? response : "Nullresponse");
        }

        private void DateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DateCheckBox.Content.ToString() + " отмечен");
            this.ToTextBlock.Visibility = Visibility.Visible;
            this.ToDatePicker.Visibility = Visibility.Visible;
            this.DateCheckBox.IsEnabled = false;
        }

        private void DateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ToTextBlock.Visibility = Visibility.Hidden;
            this.ToDatePicker.Visibility = Visibility.Hidden;
        }

        private void SaveMatchesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewStatisticsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}