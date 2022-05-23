using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook
{
    internal class EquipmentCategoryViewModel:BaseViewModel
    {
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;

        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged("EquipmentCategoryList"); }
        }
        public EquipmentCategory SelectedEquipmentCategory
        {
            get { return _selectedEquipmentCategory; }
            set { _selectedEquipmentCategory = value; OnPropertyChanged("SelectedEquipmentCategory"); }
        }

        public EquipmentCategoryViewModel()
        {
            db = new ApplicationContext();

            db.EquipmentCategories.ToList();
            _equipmentCategoryList = db.EquipmentCategories.Local.ToBindingList();
        }
    }
}
