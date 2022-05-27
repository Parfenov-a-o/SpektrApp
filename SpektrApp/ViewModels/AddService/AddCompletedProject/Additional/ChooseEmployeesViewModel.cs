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
            _allEmployeeList = db.Employees.ToList();
            _availableEmployeeList = _allEmployeeList.OrderBy(e=>e.FullName);
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
                      if((string)o == String.Empty)
                      {
                          AvailableEmployeeList = AllEmployeeList;
                          return;
                      }
                      
                      string _enternedName = SearchingEmployeeName;

                      AvailableEmployeeList = AllEmployeeList.Where(c => c.FullName.Contains(SearchingEmployeeName)).ToList();


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

                      List<Employee> _selectedEmplList = new List<Employee>();
                      _selectedEmplList.Add(employee);
                      SelectedEmployeeList = _selectedEmplList;
                      List<Employee> _availableEmplList2 = AvailableEmployeeList.ToList();
                      _availableEmplList2.Remove(employee);
                      AvailableEmployeeList = _availableEmplList2;
                      List<Employee> _allEmplList = AllEmployeeList.ToList();
                      _allEmplList.Remove(employee);
                      AllEmployeeList = _allEmplList;


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
                      // получаем выделенный объект
                      //Employee employee = selectedItem as Employee;

                      List<Employee> _selectedEmplList = new List<Employee>();
                      _selectedEmplList.AddRange(AvailableEmployeeList);
                      SelectedEmployeeList = _selectedEmplList;
                      List<Employee> _allEmplList = AllEmployeeList.ToList();
                      foreach (var empl in AvailableEmployeeList)
                      {
                          _allEmplList.Remove(empl);
                      }
                      AllEmployeeList = _allEmplList;
                      //List<Employee> _availableEmplList2 = AvailableEmployeeList.ToList();
                      //_availableEmplList2.Clear();
                      //AvailableEmployeeList = _availableEmplList2;
                      AvailableEmployeeList.ToList().Clear();
                      //List<Employee> _allEmplList = AllEmployeeList.ToList();
                      
                      //_allEmplList.Re
                      //_allEmplList.RemoveAll(AvailableEmployeeList.ToList());
                      


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

                      List<Employee> _selectedEmplList = SelectedEmployeeList.ToList();
                      _selectedEmplList.Remove(employee);
                      SelectedEmployeeList = _selectedEmplList;
                      List<Employee> _allEmplList = AllEmployeeList.ToList();
                      _allEmplList.Add(employee);
                      AllEmployeeList = _allEmplList.OrderBy(e=>e.FullName);


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

                      List<Employee> _allEmplList = AllEmployeeList.ToList();
                      foreach (var empl in SelectedEmployeeList)
                      {
                          _allEmplList.Remove(empl);
                      }
                      AllEmployeeList = _allEmplList;

                      SelectedEmployeeList.ToList().Clear();


                  }));
            }
        }



    }
}
