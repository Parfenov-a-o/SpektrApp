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
using SpektrApp.Views.AddService.AddCompletedProject;
using SpektrApp.Views.AddService.AddMaintainedCompletedProject;
using SpektrApp.Views.InfoUser;

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

        private void EmployeeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //EmployeeMenuItem.IsEnabled = false;
            EmployeeView view = new EmployeeView();
            if (view.ShowDialog() == true)
            {
                //EmployeeMenuItem.IsEnabled = true;
            }
        }

        private void PositionEmployeeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //PositionEmployeeMenuItem.IsEnabled = false;
            PositionEmployeeView view = new PositionEmployeeView();
            if (view.ShowDialog() == true)
            {
                //PositionEmployeeMenuItem.IsEnabled = true;
            }
        }
        
        private void EquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //EquipmentMenuItem.IsEnabled = false;
            EquipmentView view = new EquipmentView();
            if (view.ShowDialog() == true)
            {
                //EquipmentMenuItem.IsEnabled = true;
            }
        }

        private void CategoryEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //CategoryEquipmentMenuItem.IsEnabled = false;
            CategoryEquipmentView view = new CategoryEquipmentView();
            if (view.ShowDialog() == true)
            {
                //CategoryEquipmentMenuItem.IsEnabled = true;
            }
        }

        private void AddCompletedProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //AddCompletedProjectMenuItem.IsEnabled = false;
            AddCompletedProjectView view = new AddCompletedProjectView();
            if (view.ShowDialog() == true)
            {
                //AddCompletedProjectMenuItem.IsEnabled = true;
            }
        }

        private void AddMaintainedCompletedProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //AddMaintainedCompletedProjectMenuItem.IsEnabled = false;
            AddMaintainedCompletedProjectView view = new AddMaintainedCompletedProjectView();
            if (view.ShowDialog() == true)
            {
                //AddMaintainedCompletedProjectMenuItem.IsEnabled = true;
            }
        }

        private void UserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //UserMenuItem.IsEnabled = false;
            UserView view = new UserView();
            if (view.ShowDialog() == true)
            {
                //UserMenuItem.IsEnabled = true;
            }
        }

        private void UserRoleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //UserRoleMenuItem.IsEnabled = false;
            UserRoleView view = new UserRoleView();
            if (view.ShowDialog() == true)
            {
                //UserRoleMenuItem.IsEnabled = true;
            }
        }
    }
}
