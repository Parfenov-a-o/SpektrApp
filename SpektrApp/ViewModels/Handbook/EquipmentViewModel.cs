using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook
{
    internal class EquipmentViewModel:BaseViewModel
    {
        private IEnumerable<Equipment> _equipmentList;
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private Equipment _selectedEquipment;

        public IEnumerable<Equipment> EquipmentList
        {
            get { return _equipmentList; }
            set { _equipmentList = value; OnPropertyChanged("EquipmentList"); }
        }
        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged("EquipmentCategoryList"); }
        }
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; OnPropertyChanged("SelectedEquipment"); }
        }

        public EquipmentViewModel()
        {
            db = new ApplicationContext();

            db.Equipments.ToList();
            db.EquipmentCategories.ToList();

            _equipmentList = db.Equipments.Local.ToBindingList();
            _equipmentCategoryList = db.EquipmentCategories.Local.ToBindingList();
        }

    }
}
