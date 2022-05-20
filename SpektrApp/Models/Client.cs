using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Клиент"
    internal class Client:BaseModel
    {
        private string? _name;
        private string? _address;
        private string? _email;
        private string? _phoneNumber;
        private string? _contacts;

        //Код клиента
        public int Id { get; set; }
        //Наименование клиента
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        //Адрес клиента
        public string? Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged("Address"); }
        }
        //Электронная почта клиента
        public string? Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }
        //Номер телефона
        public string? PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }
        //Контактное лицо
        public string? Contacts
        {
            get { return _contacts; }
            set { _contacts = value; OnPropertyChanged("Contacts"); }
        }
        //Список монтажных проектов
        public List<CompletedProject> CompletedProjects { get; set; } = new();
        //Список обслуживаемых объектов
        public List<MaintainedObject> MaintainedObjects { get; set; } = new();
    }
}
