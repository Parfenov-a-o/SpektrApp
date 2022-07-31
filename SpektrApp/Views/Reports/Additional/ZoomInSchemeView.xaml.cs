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
using SpektrApp.ViewModels.Reports.Additional;

namespace SpektrApp.Views.Reports.Additional
{
    /// <summary>
    /// Логика взаимодействия для ZoomInSchemeView.xaml
    /// </summary>
    public partial class ZoomInSchemeView : Window
    {
        internal ZoomInSchemeView(ZoomInSchemeViewModel VM)
        {
            InitializeComponent();

            this.DataContext = VM;
        }
    }
}
