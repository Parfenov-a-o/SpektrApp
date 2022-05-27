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
using System.Windows.Shapes;
using SpektrApp.ViewModels.AddService.AddCompletedProject.Additional;

namespace SpektrApp.Views.AddService.AddCompletedProject.Additional
{
    /// <summary>
    /// Логика взаимодействия для SearchClientAdditionalView.xaml
    /// </summary>
    public partial class SearchClientAdditionalView : Window
    {
        internal SearchClientAdditionalView(SearchClientViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientViewModel vm = this.DataContext as SearchClientViewModel;
            if(vm.SelectedClient!=null)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Вы не выбрали клиента!");
            }
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
