﻿using System;
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
    /// Логика взаимодействия для EquipmentEditInfoView.xaml
    /// </summary>
    public partial class EquipmentEditInfoView : Window
    {
        internal EquipmentEditInfoView(EquipmentEditInfoViewModel equipvm)
        {
            InitializeComponent();

            this.DataContext = equipvm;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
