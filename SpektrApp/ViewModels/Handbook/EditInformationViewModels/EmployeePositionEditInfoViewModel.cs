using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    //ViewModel для окна "Изменить ифнормацию о должности сотрудников"
    internal class EmployeePositionEditInfoViewModel:BaseViewModel
    {
        private EmployeePosition _employeePosition;

        //Изменяемая сущность "Должность сотрудника"
        public EmployeePosition EmployeePosition
        {
            get { return _employeePosition; }
            set { _employeePosition = value; OnPropertyChanged("EmployeePosition"); }
        }
        //Конструктор с параметром типа "Должность сотрудника"
        public EmployeePositionEditInfoViewModel(EmployeePosition emplpos)
        {
            _employeePosition = emplpos;
        }
    }
}
