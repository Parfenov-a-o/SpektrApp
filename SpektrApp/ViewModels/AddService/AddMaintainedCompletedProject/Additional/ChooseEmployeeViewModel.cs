using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject.Additional
{
    internal class ChooseEmployeeViewModel:BaseViewModel
    {
        private RelayCommand _chooseEmployeeCommand;
        private RelayCommand _searchEmployeeCommand;
        private string _searchingEmployeeName;
        private IEnumerable<Employee> _employeeList;
        private Employee _selectedEmployee;

        public string SearchingEmployeeName
        {
            get { return _searchingEmployeeName; }
            set { _searchingEmployeeName = value; OnPropertyChanged(nameof(SearchingEmployeeName)); }
        }

        public IEnumerable<Employee> EmployeeList
        {
            get { return _employeeList; }
            set { _employeeList = value; OnPropertyChanged(nameof(EmployeeList)); }
        }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }

        public ChooseEmployeeViewModel()
        {
            db = new ApplicationContext();
            _employeeList = db.Employees.ToList();
            _searchingEmployeeName = "";

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
                          EmployeeList = db.Employees.ToList();
                      }
                      else
                      {
                          EmployeeList = db.Employees.Where(c => c.FullName.Contains(SearchingEmployeeName)).ToList();

                      }


                  }));
            }
        }


    }
}
