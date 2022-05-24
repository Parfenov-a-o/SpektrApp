using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    internal class EmployeeEditInfoViewModel:BaseViewModel
    {
        private Employee _employee;
        private IEnumerable<EmployeePosition> _employeePositionList;
        private EmployeePosition _selectedEmployeePosition;

        public Employee Employee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChanged(nameof(Employee)); }
        }

        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged(nameof(EmployeePositionList)); }
        }

        public EmployeePosition SelectedEmployeePosition
        {
            get { return _selectedEmployeePosition; }
            set { _selectedEmployeePosition = value; OnPropertyChanged(nameof(SelectedEmployeePosition)); }
        }

        public EmployeeEditInfoViewModel(Employee empl)
        {
            _employee = empl;

            db = new ApplicationContext();
            //db.EmployeePositions.ToList();
            _employeePositionList = db.EmployeePositions.ToList();
            _selectedEmployeePosition = empl.EmployeePosition;
        }
    }
}
