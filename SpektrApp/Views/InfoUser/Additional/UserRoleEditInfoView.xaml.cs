using SpektrApp.ViewModels.InfoUser.Additional;
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

namespace SpektrApp.Views.InfoUser.Additional
{
    /// <summary>
    /// Логика взаимодействия для UserRoleEditInfoView.xaml
    /// </summary>
    public partial class UserRoleEditInfoView : Window
    {
        internal UserRoleEditInfoView(UserRoleEditInfoViewModel VM)
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
            this.DialogResult = true;
        }
    }
}
