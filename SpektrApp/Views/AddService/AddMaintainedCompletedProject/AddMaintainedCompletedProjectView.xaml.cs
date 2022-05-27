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
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
