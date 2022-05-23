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
    /// Логика взаимодействия для ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        public ClientView()
        {
            InitializeComponent();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationContext db = new ApplicationContext();

            //db.Clients.Add(new Models.Client() { Name = "Высшая школа экономики" });

            //Models.EquipmentCategory ct1 = new Models.EquipmentCategory() { Name = "Датчики" };
            //Models.EquipmentCategory ct2 = new Models.EquipmentCategory() { Name = "Приборы" };

            //Models.Equipment eq1 = new Models.Equipment() { Name = "ДИП-31", Units = "Шт.", EquipmentCategory = ct1 };
            //Models.Equipment eq2 = new Models.Equipment() { Name = "C2000-М", Units = "Шт.", EquipmentCategory = ct2 };

            //Models.EmployeePosition position1 = new Models.EmployeePosition() { Name = "Директор" };
            //Models.EmployeePosition position2 = new Models.EmployeePosition() { Name = "Бухгалтер" };
            //Models.EmployeePosition position3 = new Models.EmployeePosition() { Name = "Сотрудник монтажной бригады" };

            //Models.Employee empl1 = new Models.Employee() { Surname = "Парфенов", FirstName = "Олег", Patronymic = "Алексеевич", Gender = "Мужской", EmployeePosition = position1 };
            //Models.Employee empl2 = new Models.Employee() { Surname = "Парфенова", FirstName = "Светлана", Patronymic = "Михайловна", Gender = "Женский", EmployeePosition = position2 };
            //Models.Employee empl3 = new Models.Employee() { Surname = "Баранов", FirstName = "Антон", Patronymic = "Анатольевич", Gender = "Мужской", EmployeePosition = position3 };


            //db.EquipmentCategories.AddRange(ct1,ct2);
            //db.Equipments.AddRange(eq1,eq2);
            //db.EmployeePositions.AddRange(position1,position2,position3);
            //db.Employees.AddRange(empl1, empl2, empl3);
            //db.SaveChanges();
        }
    }
}
