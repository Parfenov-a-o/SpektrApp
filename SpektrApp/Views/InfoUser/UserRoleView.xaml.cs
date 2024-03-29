﻿using SpektrApp.ViewModels.InfoUser;
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

namespace SpektrApp.Views.InfoUser
{
    /// <summary>
    /// Логика взаимодействия для UserRoleView.xaml
    /// </summary>
    public partial class UserRoleView : Window
    {
        public UserRoleView()
        {
            InitializeComponent();

            this.DataContext = new UserRoleViewModel();
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
