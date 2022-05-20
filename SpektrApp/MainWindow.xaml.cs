using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpektrApp.Views.Handbook;

namespace SpektrApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //ClientMenuItem.IsEnabled = false;
            ClientView view = new ClientView();
            if(view.ShowDialog() == true)
            {
                //ClientMenuItem.IsEnabled = true;
            }
        }
    }
}
