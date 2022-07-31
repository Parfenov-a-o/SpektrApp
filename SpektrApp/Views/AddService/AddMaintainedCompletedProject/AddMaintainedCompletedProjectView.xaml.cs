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
using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject;

namespace SpektrApp.Views.AddService.AddMaintainedCompletedProject
{
    /// <summary>
    /// Логика взаимодействия для AddServiceCompletedProjectView.xaml
    /// </summary>
    public partial class AddMaintainedCompletedProjectView : Window
    {
        public AddMaintainedCompletedProjectView()
        {
            InitializeComponent();

            this.DataContext = new AddMaintainedCompletedProjectViewModel();
        }
        internal AddMaintainedCompletedProjectView(AddMaintainedCompletedProjectViewModel VM)
        {
            InitializeComponent();

            this.DataContext = VM;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = true;

            //AddMaintainedCompletedProjectViewModel vm = this.DataContext as AddMaintainedCompletedProjectViewModel;
            
            //if (vm.MaintainedObject.Client != null)
            //{
            //    this.DialogResult = true;
            //}
            //else
            //{
            //    MessageBox.Show("Вы не выбрали клиента!");
            //}
        }
    }
}
