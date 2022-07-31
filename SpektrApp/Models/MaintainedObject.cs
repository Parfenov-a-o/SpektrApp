using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Обслуживаемый объект"
    internal class MaintainedObject:BaseModel
    {
        private DateTime? _serviceStartDate;
        private DateTime? _serviceEndDate;
        private int _clientId;
        private Client? _client;
        private string? _address;
        private List<InstalledEquipment> _installedEquipments = new();
        private string? _objectDescription;
        private int _employeeId;
        private Employee? _employee;
        private string? _objectSchema;

        //Код обслуживаемого объекта
        public int Id { get; set; }
        //Дата начала обслуживания
        public DateTime? ServiceStartDate
        {
            get { return _serviceStartDate; }
            set { _serviceStartDate = value; OnPropertyChanged(nameof(ServiceStartDate)); }
        }
        //Дата окончания обслуживания
        public DateTime? ServiceEndDate
        {
            get { return _serviceEndDate; }
            set { _serviceEndDate = value; OnPropertyChanged(nameof(ServiceEndDate)); }
        }
        //Код клиента
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; OnPropertyChanged(nameof(ClientId)); }
        }
        //Ссылка на клиента
        public Client? Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged(nameof(Client)); }
        }
        //Адрес
        public string? Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }
        //Список установленного на объекте оборудования
        public List<InstalledEquipment> InstalledEquipments
        {
            get { return _installedEquipments; }
            set { _installedEquipments = value; OnPropertyChanged(nameof(InstalledEquipments)); }
        }
        //Дополнительное описание объекта
        public string? ObjectDescription
        {
            get { return _objectDescription; }
            set { _objectDescription = value; OnPropertyChanged(nameof(ObjectDescription)); }
        }
        //Код сотрудника
        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; OnPropertyChanged(nameof(EmployeeId)); }
        }
        //Ссылка на сотрудника
        public Employee? Employee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChanged(nameof(Employee)); }
        }
        //Схема объекта
        public string? ObjectSchema
        {
            get { return _objectSchema; }
            set { _objectSchema = value; OnPropertyChanged(nameof(ObjectSchema)); }
        }
    }
}
