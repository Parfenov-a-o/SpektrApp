using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject;
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

namespace SpektrApp.Views.Reports.Additional
{
    /// <summary>
    /// Логика взаимодействия для EditInfoAboutMaintainedObjectView.xaml
    /// </summary>
    public partial class EditInfoAboutMaintainedObjectView : Window
    {
        public EditInfoAboutMaintainedObjectView()
        {
            InitializeComponent();
        }

        internal EditInfoAboutMaintainedObjectView(AddMaintainedCompletedProjectViewModel VM)
        {
            InitializeComponent();

            this.DataContext = VM;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
