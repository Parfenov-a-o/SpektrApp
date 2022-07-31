using SpektrApp.ViewModels.AddService;
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
    /// Логика взаимодействия для EditInfoAboutCompletedProjectView.xaml
    /// </summary>
    public partial class EditInfoAboutCompletedProjectView : Window
    {
        public EditInfoAboutCompletedProjectView()
        {
            InitializeComponent();
        }

        internal EditInfoAboutCompletedProjectView(AddCompletedProjectViewModel VM)
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
