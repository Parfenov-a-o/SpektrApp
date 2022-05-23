using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook
{
    internal class EmployeePositionViewModel:BaseViewModel
    {
        private IEnumerable<EmployeePosition> _employeePositionList;
        private EmployeePosition _selectedEmployeePosition;

        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged("EmployeePositionList"); }
        }
        public EmployeePosition SelectedEmployeePosition
        {
            get { return _selectedEmployeePosition; }
            set { _selectedEmployeePosition = value; OnPropertyChanged("SelectedEmployeePosition"); }
        }

        public EmployeePositionViewModel()
        {
            db = new ApplicationContext();

            db.EmployeePositions.ToList();
            _employeePositionList = db.EmployeePositions.Local.ToBindingList();
        }
    }
}
