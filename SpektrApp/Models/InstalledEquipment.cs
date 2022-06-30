using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Установленное на объекте оборудование"
    internal class InstalledEquipment:BaseModel
    {
        private int _indexNumber;
        private double _count;
        private int _equipmentId;
        private Equipment? _equipment;

        public int Id { get; set; }
        //Номер позиции
        public int IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value; OnPropertyChanged("IndexNumber"); }
        }
        //Количество установленного оборудования
        public double Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }
        //Код оборудования
        public int EquipmentId
        {
            get { return _equipmentId; }
            set { _equipmentId = value; OnPropertyChanged("EquipmentId"); }
        }
        //Ссылка на оборудование
        public Equipment? Equipment
        {
            get { return _equipment; }
            set { _equipment = value; OnPropertyChanged("Equipment"); }
        }

        //Список монтажных проектов
        public List<CompletedProject> CompletedProjects { get; set; } = new();
        //Список обслуживаемых объектов
        public List<MaintainedObject> MaintainedObjects { get; set; } = new();

    }
}
