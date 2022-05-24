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
using SpektrApp.ViewModels.Handbook;

namespace SpektrApp.Views.Handbook
{
    /// <summary>
    /// Логика взаимодействия для CategoryEquipmentView.xaml
    /// </summary>
    public partial class CategoryEquipmentView : Window
    {
        public CategoryEquipmentView()
        {
            InitializeComponent();
            this.DataContext = new EquipmentCategoryViewModel();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
