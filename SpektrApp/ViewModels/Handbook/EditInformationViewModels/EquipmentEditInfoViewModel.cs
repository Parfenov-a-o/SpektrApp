using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    internal class EquipmentEditInfoViewModel:BaseViewModel
    {
        private Equipment _equipment;
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;

        public Equipment Equipment
        {
            get { return _equipment; }
            set { _equipment = value; OnPropertyChanged(nameof(Equipment)); }
        }
        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged(nameof(EquipmentCategoryList)); }
        }
        public EquipmentCategory SelectedEquipmentCategory
        {
            get { return _selectedEquipmentCategory; }
            set { _selectedEquipmentCategory = value; OnPropertyChanged(nameof(SelectedEquipmentCategory)); }
        }

        public EquipmentEditInfoViewModel(Equipment equip)
        {
            _equipment = equip;

            db = new ApplicationContext();
            _equipmentCategoryList = db.EquipmentCategories.ToList();
            if(equip.Id!=0)
            {
                _selectedEquipmentCategory = db.EquipmentCategories.Find(equip.EquipmentCategory.Id);
            }

            //_selectedEquipmentCategory = equip.EquipmentCategory;
        }
    }
}
