using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    //ViewModel для окна "Изменить сведения об оборудовании"
    internal class EquipmentEditInfoViewModel:BaseViewModel
    {
        private Equipment _equipment;
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;

        //Изменяемая сущность "Оборудование"
        public Equipment Equipment
        {
            get { return _equipment; }
            set { _equipment = value; OnPropertyChanged(nameof(Equipment)); }
        }
        //Список категорий оборудования
        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged(nameof(EquipmentCategoryList)); }
        }
        //Выбранная категория оборудования
        public EquipmentCategory SelectedEquipmentCategory
        {
            get { return _selectedEquipmentCategory; }
            set { _selectedEquipmentCategory = value; OnPropertyChanged(nameof(SelectedEquipmentCategory)); }
        }
        //Конструктор с параметром типа "Equipment"
        public EquipmentEditInfoViewModel(Equipment equip)
        {
            _equipment = equip;

            db = new ApplicationContext();
            _equipmentCategoryList = db.EquipmentCategories.ToList();
            if(equip.Id!=0)
            {
                _selectedEquipmentCategory = db.EquipmentCategories.Find(equip.EquipmentCategory.Id);
            }
        }
    }
}
