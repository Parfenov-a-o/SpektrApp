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
    /// Логика взаимодействия для ChoiceEmployeeInstallationAdditionalView.xaml
    /// </summary>
    public partial class ChoiceEmployeeInstallationAdditionalView : Window
    {
        public ChoiceEmployeeInstallationAdditionalView()
        {
            InitializeComponent();
            this.DataContext = new ChooseEmployeesViewModel();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
