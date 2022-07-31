using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Должность сотрудника"
    internal class EmployeePosition:BaseModel
    {
        private string? _name;

        //Код должности
        public int Id { get; set; }
        //Наименование должности
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        //Список сотрудников
        public List<Employee> Employees { get; set; } = new();

        
    }
}
