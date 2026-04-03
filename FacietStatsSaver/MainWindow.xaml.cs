using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            //var response = await _model.LastMatchesAsync(); 
            debugWnd.Show();
            debugWnd.ShowViewModel(await _model.LastMatchesAsync());
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
    }
}