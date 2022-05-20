using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Сотрудник"
    internal class Employee:BaseModel
    {
        private string? _surname;
        private string? _firstName;
        private string? _patronymic;
        private DateTime? _dateOfBirth;
        private string? _gender;
        private string? _email;
        private string? _phoneNumber;
        private int _employeePositionId;
        private EmployeePosition? _employeePosition;
        
        //Код сотрудника
        public int Id { get; set; }
        //Фамилия сотрудника
        public string? Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged("Surname"); }
        }
        //Имя сотрудника
        public string? FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }
        //Отчество сотрудника
        public string? Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; OnPropertyChanged(nameof(Patronymic));}
        }
        //Дата рождения сотрудника
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; OnPropertyChanged(nameof(DateOfBirth)); }
        }
        //Пол сотрудника
        public string? Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }
        //Адрес электронной почты сотрудника
        public string? Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        //Номер телефона
        public string? PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }
        //Код должности
        public int EmployeePositionId
        {
            get { return _employeePositionId; }
            set { _employeePositionId = value; OnPropertyChanged(nameof(EmployeePositionId)); }
        }
        //Ссылка на должность
        public EmployeePosition EmployeePosition
        {
            get { return _employeePosition; }
            set { _employeePosition = value; OnPropertyChanged(nameof(EmployeePosition)); }
        }


        //Список монтажных проектов
        public List<CompletedProject> CompletedProjects { get; set; } = new();
        //Список обслуживаемых объектов
        public List<MaintainedObject> MaintainedObjects { get; set; } = new();

    }
}
