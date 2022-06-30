using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    internal class ChooseEmployeesViewModel:BaseViewModel
    {
        private RelayCommand _searchEmployeeCommand;
        private RelayCommand _addSelectedEmployeeCommand;
        private RelayCommand _addAllEmployeesCommand;
        private RelayCommand _removeSelectedEmployeeCommand;
        private RelayCommand _removeAllEmployeesCommand;
        

        private IEnumerable<Employee> _availableEmployeeList;
        private IEnumerable<Employee> _selectedEmployeeList;
        private IEnumerable<Employee> _allEmployeeList;
        private Employee _selectedEmployee;
        private Employee _selectedEmployeeInSelectedBox;
        private string _searchingEmployeeName;

        public IEnumerable<Employee> SelectedEmployeeList
        {
            get { return _selectedEmployeeList; }
            set { _selectedEmployeeList = value; OnPropertyChanged(nameof(SelectedEmployeeList)); }
        }

        public IEnumerable<Employee> AvailableEmployeeList
        {
            get { return _availableEmployeeList; }
            set { _availableEmployeeList = value; OnPropertyChanged(nameof(AvailableEmployeeList)); }
        }
        public IEnumerable<Employee> AllEmployeeList
        {
            get { return _allEmployeeList; }
            set { _allEmployeeList = value; OnPropertyChanged(nameof(AllEmployeeList)); }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        public Employee SelectedEmployeeInSelectedBox
        {
            get { return _selectedEmployeeInSelectedBox; }
            set { _selectedEmployeeInSelectedBox = value; OnPropertyChanged(nameof(SelectedEmployeeInSelectedBox));}
        }

        public string SearchingEmployeeName
        {
            get { return _searchingEmployeeName; }
            set { _searchingEmployeeName = value; OnPropertyChanged(nameof(SearchingEmployeeName));}
        }

        public ChooseEmployeesViewModel()
        {
            db = new ApplicationContext();
            _searchingEmployeeName = "";
            db.Employees.ToList();
            _allEmployeeList = db.Employees.Local.ToList();
            db.EmployeePositions.ToList();
            _availableEmployeeList = _allEmployeeList.OrderBy(e => e.FullName);
            _selectedEmployeeList = new List<Employee>();
        }


        //Команда для поиска
        public RelayCommand SearchEmployeeCommand
        {
            get
            {
                return _searchEmployeeCommand ??
                  (_searchEmployeeCommand = new RelayCommand((o) =>
                  {
                      if(SearchingEmployeeName == "")
                      {
                          AvailableEmployeeList = AllEmployeeList;
                          return;
                      }
                      
                      string _enternedName = SearchingEmployeeName;

                      AvailableEmployeeList = AllEmployeeList.Where(c => c.FullName.ToLower().Contains(SearchingEmployeeName.ToLower())).ToList();


                  }));
            }
        }


        public RelayCommand AddSelectedEmployeeCommand
        {
            get
            {
                return _addSelectedEmployeeCommand ??
                  (_addSelectedEmployeeCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали сотрудника!");
                          return;
                      }
                      // получаем выделенный объект
                      Employee employee = selectedItem as Employee;

                      
                      //Добавление выбранного сотрудника в список выбранных сотрудников
                      SelectedEmployeeList = SelectedEmployeeList.Append(employee)!;
                      //Исключение добавленных сотрудников из списка доступных сотрудников
                      AvailableEmployeeList = AvailableEmployeeList.Except(SelectedEmployeeList);
                      //Исключение добавленных сотрудников из списка всех сотрудников
                      AllEmployeeList = AllEmployeeList.Except(SelectedEmployeeList);

                  }));
            }
        }


        public RelayCommand AddAllEmployeesCommand
        {
            get
            {
                return _addAllEmployeesCommand ??
                  (_addAllEmployeesCommand = new RelayCommand((o) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (AvailableEmployeeList.Count()==0)
                      {
                          MessageBox.Show("В списке нет сотрудников!");
                          return;
                      }

                      //Перебор всех найденных сотрудников
                      foreach(var item in AvailableEmployeeList)
                      {
                          //Добавление всех найденных сотрудников
                          SelectedEmployeeList = SelectedEmployeeList.Append(item)!;
                      }
                      //Исключение добавленных сотрудников из списка доступных сотрудников
                      AvailableEmployeeList = AvailableEmployeeList.Except(SelectedEmployeeList);
                      //Исключение добавленных сотрудников из списка всех сотрудников
                      AllEmployeeList = AllEmployeeList.Except(SelectedEmployeeList);                   


                  }));
            }
        }


        public RelayCommand RemoveSelectedEmployeeCommand
        {
            get
            {
                return _removeSelectedEmployeeCommand ??
                  (_removeSelectedEmployeeCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали сотрудника!");
                          return;
                      }


                      // получаем выделенный объект
                      Employee employee = selectedItem as Employee;

                      AllEmployeeList = AllEmployeeList.Append(employee)!;
                      AvailableEmployeeList = AvailableEmployeeList.Append(employee)!;
                      SelectedEmployeeList = SelectedEmployeeList.Except(AllEmployeeList);
                      AvailableEmployeeList = AllEmployeeList.Where(c => c.FullName.Contains(SearchingEmployeeName)).ToList();


                  }));
            }
        }

        public RelayCommand RemoveAllEmployeesCommand
        {
            get
            {
                return _removeAllEmployeesCommand ??
                  (_removeAllEmployeesCommand = new RelayCommand((o) =>
                  {
                      if (SelectedEmployeeList.Count() == 0)
                      {
                          MessageBox.Show("В списке нет сотрудников!");
                          return;
                      }

                      //Перебор всех выбранных сотрудников
                      foreach (var item in SelectedEmployeeList)
                      {
                          //Возвращение в список доступных сотрудников
                          AllEmployeeList = AllEmployeeList.Append(item)!;
                          
                      }
                      //Очистка списка выбранных сотрудников
                      SelectedEmployeeList = SelectedEmployeeList.Except(AllEmployeeList);
                      
                      AvailableEmployeeList = AllEmployeeList.Where(c => c.FullName.Contains(SearchingEmployeeName)).ToList();
                      

                  }));
            }
        }






    }
}
