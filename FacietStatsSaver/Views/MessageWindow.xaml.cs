using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FacietStatsSaver.ViewModel;

namespace FacietStatsSaver
{
    /// <summary>
    /// Логика взаимодействия для debugWnd.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string Message)
        {
            InitializeComponent();


            MessageViewModel vm = new MessageViewModel(Message);

            vm.CloseAction = new Action(this.Close);

            DataContext = vm;
        }

    }
}
