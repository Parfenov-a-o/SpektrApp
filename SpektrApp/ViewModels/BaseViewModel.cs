using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SpektrApp.Models;
//using SpektrApp.Views.DialogWindows;
using System.Windows;


namespace SpektrApp.ViewModels
{
    internal class BaseViewModel:INotifyPropertyChanged
    {
        protected ApplicationContext db;
        protected RelayCommand deleteCommand;
        

        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {

                      //Если параметр команды был пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали запись для удаления!");
                          return;
                      }

                      // получаем выделенный объект
                      BaseModel employee = selectedItem as BaseModel;

                      //Создание окна подтверждения удаления
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить выбранный элемент?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                      if (result == MessageBoxResult.Yes)
                      {
                          //Удаление выбранной записи
                          switch (selectedItem.GetType().Name)
                          {
                              case "Client":
                                  db.Clients.Remove(selectedItem as Client);
                                  break;
                              case "Employee":
                                  db.Employees.Remove(selectedItem as Employee);
                                  break;
                              case "EmployeePosition":
                                  db.EmployeePositions.Remove(selectedItem as EmployeePosition);
                                  break;
                              case "Equipment":
                                  db.Equipments.Remove(selectedItem as Equipment);
                                  break;
                              case "EquipmentCategory":
                                  db.EquipmentCategories.Remove(selectedItem as EquipmentCategory);
                                  break;
                          }
                          db.SaveChanges();
                          
                      }


                  }));
            }
        }





        //Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
