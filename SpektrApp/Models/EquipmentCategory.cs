using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpektrApp.Models
{
    //Модель "Категория оборудования"
    internal class EquipmentCategory:BaseModel
    {
        private string? _name;

        //Код категории
        public int Id { get; set; }
        //Наименование категории оборудования
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        //Список оборудования
        public List<Equipment> Equipments { get; set; } = new();
        
    }
}
