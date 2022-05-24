using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    internal class EmployeePositionEditInfoViewModel:BaseViewModel
    {
        private EmployeePosition _employeePosition;

        public EmployeePosition EmployeePosition
        {
            get { return _employeePosition; }
            set { _employeePosition = value; OnPropertyChanged("EmployeePosition"); }
        }

        public EmployeePositionEditInfoViewModel(EmployeePosition emplpos)
        {
            _employeePosition = emplpos;
        }
    }
}
