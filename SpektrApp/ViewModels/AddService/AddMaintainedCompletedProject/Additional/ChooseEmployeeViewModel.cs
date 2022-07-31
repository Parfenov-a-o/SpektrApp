using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject.Additional
{
    //ViewModel для окна "Поиск сотрудника"
    internal class ChooseEmployeeViewModel:BaseViewModel
    {

        private RelayCommand _searchEmployeeCommand;
        private RelayCommand _chooseEmployeeCommand;
        private string _searchingEmployeeName;
        private List<Employee> _employeeList;
        private List<Employee> _allEmployeeList;
        private Employee _selectedEmployee;

        //Введённое в строке поиска значение
        public string SearchingEmployeeName
        {
            get { return _searchingEmployeeName; }
            set { _searchingEmployeeName = value; OnPropertyChanged(nameof(SearchingEmployeeName)); }
        }

        //Список найденных сотрудников
        public List<Employee> EmployeeList
        {
            get { return _employeeList; }
            set { _employeeList = value; OnPropertyChanged(nameof(EmployeeList)); }
        }
        //Список всех доступных для поиска сотрудников
        public List<Employee> AllEmployeeList
        {
            get { return _allEmployeeList; }
            set { _allEmployeeList = value; OnPropertyChanged(nameof(AllEmployeeList)); }
        }
        //Выбранный сотрудник
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        //Конструктор без параметров
        public ChooseEmployeeViewModel()
        {
            using(db = new ApplicationContext())
            {
                db.Employees.ToList();
                db.EmployeePositions.ToList();

                _employeeList = db.Employees.Local.ToList();
                _allEmployeeList = db.Employees.Local.ToList();
            }
            _searchingEmployeeName = "";

        }

        //Команда для поиска сотрудника
        public RelayCommand SearchEmployeeCommand
        {
            get
            {
                return _searchEmployeeCommand ??
                  (_searchEmployeeCommand = new RelayCommand((o) =>
                  {
                      //Очищаем результаты предыдущего поиска
                      EmployeeList.Clear();
                      if(SearchingEmployeeName == "")
                      {
                          //Если строка поиска пустая, то выводим всех доступных для поиска сотрудников
                          EmployeeList = AllEmployeeList.Select(e=>e).ToList();
                      }
                      else
                      {
                          //Осуществляем поиск по совпадению со значением из строки поиска
                          EmployeeList = AllEmployeeList.Where(e => e.FullName.ToLower().Contains(SearchingEmployeeName.ToLower())).ToList();

                      }


                  }));
            }
        }



    }
}
