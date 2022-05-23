using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook
{
    internal class EmployeeViewModel:BaseViewModel
    {
        private IEnumerable<Employee> _employeeList;
        private IEnumerable<EmployeePosition> _employeePositionList;
        private Employee? _selectedEmployee;

        public IEnumerable<Employee> EmployeeList
        {
            get { return _employeeList; }
            set { _employeeList = value; OnPropertyChanged("EmployeeList"); }
        }
        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged(nameof(EmployeePositionList)); }
        }
        public Employee? SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }
        public EmployeeViewModel()
        {
            db = new ApplicationContext();

            db.Employees.ToList();
            db.EmployeePositions.ToList();

            _employeeList = db.Employees.Local.ToBindingList();
            _employeePositionList = db.EmployeePositions.Local.ToBindingList();
        }
        

    }
}
