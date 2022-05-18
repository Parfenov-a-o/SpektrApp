using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель выполненный монтажный проект
    internal class CompletedProject:BaseModel
    {
        private DateTime? _projectCompletionDate;
        private int _clientId;
        private Client? _client;
        private string? _address;
        private List<InstalledEquipment> _installedEquipments = new();
        private string? _objectDescription;
        private List<Employee> _employees = new();
        private string? _objectSchema;

        //Код выполненного проекта
        public int Id { get; set; }
        //Дата завершения проекта
        public DateTime? ProjectCompletionDate
        {
            get { return _projectCompletionDate; }
            set { _projectCompletionDate = value; OnPropertyChanged("ProjectCompletionDate"); }
        }
        //Код клиента
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; OnPropertyChanged("ClientId"); }
        }
        //Ссылка на клиента
        public Client? Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged("Client"); }
        }
        //Адрес помещения
        public string? Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged("Address"); }
        }
        //Список установленного на объекте оборудования
        public List<InstalledEquipment> InstalledEquipments
        {
            get { return _installedEquipments; }
            set { _installedEquipments = value; OnPropertyChanged(nameof(InstalledEquipments)); }
        }
        //Дополнительное описание проекта
        public string? ObjectDescription
        {
            get { return _objectDescription; }
            set { _objectDescription = value; OnPropertyChanged(nameof(ObjectDescription)); }
        }
        //Список сотрудников, осуществивших монтажный проект
        public List<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged(nameof(Employees)); }
        }
        //Чертеж проекта
        public string? ObjectSchema
        {
            get { return _objectSchema; }
            set { _objectSchema = value; OnPropertyChanged(nameof(ObjectSchema)); }
        }

        
    }
}
