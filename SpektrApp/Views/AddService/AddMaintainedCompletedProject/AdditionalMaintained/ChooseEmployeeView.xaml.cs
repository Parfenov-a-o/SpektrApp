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
using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject.Additional;



namespace SpektrApp.Views.AddService.AddMaintainedCompletedProject.AdditionalMaintained
{
    /// <summary>
    /// Логика взаимодействия для ChooseEmployeeView.xaml
    /// </summary>
    public partial class ChooseEmployeeView : Window
    {
        internal ChooseEmployeeView(ChooseEmployeeViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployeeViewModel vm = this.DataContext as ChooseEmployeeViewModel;
            if (vm.SelectedEmployee != null)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Вы не выбрали сотрудника!");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
