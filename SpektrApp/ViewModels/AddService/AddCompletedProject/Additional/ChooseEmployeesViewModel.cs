using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    //ViewModel для окна "Выбрать сотрудников, выполнивших монтажный проект"
    internal class ChooseEmployeesViewModel:BaseViewModel
    {
        private RelayCommand _searchEmployeeCommand;
        private RelayCommand _addSelectedEmployeeCommand;
        private RelayCommand _addAllEmployeesCommand;
        private RelayCommand _removeSelectedEmployeeCommand;
        private RelayCommand _removeAllEmployeesCommand;

        private Employee _selectedEmployee;
        private Employee _selectedEmployeeInSelectedBox;
        private string _searchingEmployeeName;

        //Коллекция найденных сотрудников
        public ObservableCollection<Employee> AvailableEmployeeList { get; set; } = new ObservableCollection<Employee>();
        //Коллекция выбранных сотрудников
        public ObservableCollection<Employee> SelectedEmployeeList { get; set; } = new ObservableCollection<Employee>();
        //Коллекция доступных для поиска сотрудников (все, кроме выбранных)
        public ObservableCollection<Employee> AllEmployeeList { get; set; } = new ObservableCollection<Employee>();

        //Выбранный сотрудник
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }
        //Выбранный сотрудник в окне выбранных сотрудников
        public Employee SelectedEmployeeInSelectedBox
        {
            get { return _selectedEmployeeInSelectedBox; }
            set { _selectedEmployeeInSelectedBox = value; OnPropertyChanged(nameof(SelectedEmployeeInSelectedBox));}
        }
        //Набранное имя в строке поиска
        public string SearchingEmployeeName
        {
            get { return _searchingEmployeeName; }
            set { _searchingEmployeeName = value; OnPropertyChanged(nameof(SearchingEmployeeName));}
        }

        //Конструктор без параметров
        public ChooseEmployeesViewModel()
        {
            using (db = new ApplicationContext())
            {
                _searchingEmployeeName = "";
                db.Employees.ToList();
                db.EmployeePositions.ToList();
            }

        }


        //Команда для поиска сотрудников
        public RelayCommand SearchEmployeeCommand
        {
            get
            {
                return _searchEmployeeCommand ??
                  (_searchEmployeeCommand = new RelayCommand((o) =>
                  {
                      //Очистка списка найденных сотрудников
                      AvailableEmployeeList.Clear();

                      //Если поле не заполнено, то выводятся все доступные для поиска сотрудники
                      if(SearchingEmployeeName == "")
                      {
                          foreach(var employee in AllEmployeeList)
                          {
                              AvailableEmployeeList.Add(employee);
                          }
                          return;
                      }

                      //Отбор сотрудников в соответствии с введенным значением в строке поиска
                      IEnumerable<Employee> _findingEmployees = AllEmployeeList.Where(c => c.FullName.ToLower().Contains(SearchingEmployeeName.ToLower()));

                      //Добавление найденных сотрудников в коллекцию
                      foreach (var employee in _findingEmployees)
                      {
                          AvailableEmployeeList.Add(employee);
                      }

                  }));
            }
        }

        //Команда для добавления выбранного сотрудника
        public RelayCommand AddSelectedEmployeeCommand
        {
            get
            {
                return _addSelectedEmployeeCommand ??
                  (_addSelectedEmployeeCommand = new RelayCommand((selectedItem) =>
                  {
                      using(db=new ApplicationContext())
                      {
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();
                          //Если принимаемый командой параметр пуст
                          if (selectedItem == null)
                          {
                              MessageBox.Show("Вы не выбрали сотрудника!");
                              return;
                          }
                          // получаем выделенный объект
                          if (selectedItem is Employee employee)
                          {
                              //Исключение добавленных сотрудников из списка доступных сотрудников
                              AvailableEmployeeList.Remove(AvailableEmployeeList.First(empl=>empl.Id==employee.Id));
                              //Добавление выбранного сотрудника в список выбранных сотрудников
                              SelectedEmployeeList.Add(db.Employees.Find(employee.Id));
                              //Исключение добавленных сотрудников из списка всех сотрудников
                              AllEmployeeList.Remove(AllEmployeeList.First(empl => empl.Id == employee.Id));
                          }
                      }

                  }));
            }
        }

        //Команда для добавления всех найденных сотрудников
        public RelayCommand AddAllEmployeesCommand
        {
            get
            {
                return _addAllEmployeesCommand ??
                  (_addAllEmployeesCommand = new RelayCommand((o) =>
                  {
                      using(var db=new ApplicationContext())
                      {
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();

                          //Если принимаемый командой параметр пуст
                          if (AvailableEmployeeList.Count() == 0)
                          {
                              MessageBox.Show("В списке нет сотрудников!");
                              return;
                          }

                          //Перебор всех найденных сотрудников
                          foreach (var item in AvailableEmployeeList)
                          {
                              //Добавление всех найденных сотрудников
                              SelectedEmployeeList.Add(item);
                              //Исключение добавленных сотрудников из списка всех сотрудников
                              AllEmployeeList.Remove(AllEmployeeList.First(empl => empl.Id == item.Id));
                              //Добавление всех найденных сотрудников
                          }
                          //Исключение добавленных сотрудников из списка доступных сотрудников
                          AvailableEmployeeList.Clear();

                      }




                  }));
            }
        }

        //Команда для удаления сотрудника из списка выбранных сотрудников
        public RelayCommand RemoveSelectedEmployeeCommand
        {
            get
            {
                return _removeSelectedEmployeeCommand ??
                  (_removeSelectedEmployeeCommand = new RelayCommand((selectedItem) =>
                  {
                      using (db = new ApplicationContext())
                      {
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();
                          //Если принимаемый командой параметр пуст
                          if (selectedItem == null)
                          {
                              MessageBox.Show("Вы не выбрали сотрудника!");
                              return;
                          }
                          // получаем выделенный объект
                          if (selectedItem is Employee employee)
                          {
                              //Исключение добавленных сотрудников из списка доступных сотрудников
                              AvailableEmployeeList.Add(db.Employees.Find(employee.Id));
                              //Добавление выбранного сотрудника в список выбранных сотрудников
                              SelectedEmployeeList.Remove(SelectedEmployeeList.First(empl => empl.Id == employee.Id));
                              //Исключение добавленных сотрудников из списка всех сотрудников
                              AllEmployeeList.Add(db.Employees.Find(employee.Id));
                          }
                      }


                  }));
            }
        }

        //Команда для удаления всех выбранных сотрудников из списка
        public RelayCommand RemoveAllEmployeesCommand
        {
            get
            {
                return _removeAllEmployeesCommand ??
                  (_removeAllEmployeesCommand = new RelayCommand((o) =>
                  {

                      using (var db = new ApplicationContext())
                      {
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();

                          //Если принимаемый командой параметр пуст
                          if (SelectedEmployeeList.Count() == 0)
                          {
                              MessageBox.Show("В списке нет сотрудников!");
                              return;
                          }

                          //Перебор выбранных сотрудников
                          foreach (var item in SelectedEmployeeList)
                          {
                              //Добавление сотрудников в общий список доступных для поиска сотрудников
                              AllEmployeeList.Add(db.Employees.Find(item.Id));

                              //Добавление в список найденных сотрудников только тех, которые удовлетворяют условиям поиска
                              if (item.FullName.Contains(SearchingEmployeeName))
                              {
                                  AvailableEmployeeList.Add(db.Employees.Find(item.Id));
                              }


                          }
                          //Очищаем список выбранных сотрудников
                          SelectedEmployeeList.Clear();

                      }


                  }));
            }
        }






    }
}
