using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    //ViewModel для окна "Изменить сведения о сотруднике"
    internal class EmployeeEditInfoViewModel:BaseViewModel
    {
        private Employee _employee;
        private IEnumerable<EmployeePosition> _employeePositionList;
        private EmployeePosition _selectedEmployeePosition;
        private IEnumerable<string> _genderList = new List<string>()
        {
            "Мужской","Женский"
        };

        //Изменяемая сущность "Сотрудник"
        public Employee Employee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChanged(nameof(Employee)); }
        }

        //Список должностей сотрудников
        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged(nameof(EmployeePositionList)); }
        }
        //Выбранная должность
        public EmployeePosition SelectedEmployeePosition
        {
            get { return _selectedEmployeePosition; }
            set { _selectedEmployeePosition = value; OnPropertyChanged(nameof(SelectedEmployeePosition)); }
        }
        //Список возможных полов сотрудников (Мужской/женский)
        public IEnumerable<string> GenderList
        {
            get { return _genderList; }
            set { _genderList = value; OnPropertyChanged(nameof(GenderList)); }
        }


        //Конструктор с параметром типа "Employee"
        public EmployeeEditInfoViewModel(Employee empl)
        {
            _employee = empl;
            db = new ApplicationContext();
            _employeePositionList = db.EmployeePositions.ToList();
            if(empl.Id != 0)
            {
                _selectedEmployeePosition = db.EmployeePositions.Find(empl.EmployeePosition.Id);
            }
          

        }
    }
}
