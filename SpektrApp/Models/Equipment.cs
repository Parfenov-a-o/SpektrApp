using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpektrApp.Models
{
    //Модель "Оборудование"
    internal class Equipment:BaseModel
    {
        private string? _name;
        private string? _description;
        private int _equipmentCategoryId;
        private EquipmentCategory? _equipmentCategory;
        private string? _units;

        //Код оборудования
        public int Id { get; set; }
        //Наименование оборудования
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        //Дополнительное описание оборудования
        public string? Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }
        //Код категории оборудования
        public int EquipmentCategoryId
        {
            get { return _equipmentCategoryId; }
            set { _equipmentCategoryId = value; OnPropertyChanged("EquipmentCategoryId"); }
        }
        //Ссылка на категорию оборудования
        public EquipmentCategory? EquipmentCategory
        {
            get { return _equipmentCategory; }
            set { _equipmentCategory = value; OnPropertyChanged("EquipmentCategory"); }
        }
        //Единицы измерения оборудования
        public string? Units
        {
            get { return _units; }
            set { _units = value; OnPropertyChanged(nameof(Units)); }
        }

    }
}
