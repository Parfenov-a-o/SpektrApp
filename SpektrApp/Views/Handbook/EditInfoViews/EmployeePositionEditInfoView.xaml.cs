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
using SpektrApp.ViewModels.Handbook.EditInformationViewModels;


namespace SpektrApp.Views.Handbook.EditInfoViews
{
    /// <summary>
    /// Логика взаимодействия для EmployeePositionEditInfoView.xaml
    /// </summary>
    public partial class EmployeePositionEditInfoView : Window
    {
        internal EmployeePositionEditInfoView(EmployeePositionEditInfoViewModel emplposvm)
        {
            InitializeComponent();

            this.DataContext = emplposvm;
        }
    }
}
