using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    internal class EquipmentCategoryEditInfoViewModel:BaseViewModel
    {
        private EquipmentCategory _equipmentCategory;

        public EquipmentCategory EquipmentCategory
        {
            get { return _equipmentCategory; }
            set { _equipmentCategory = value; OnPropertyChanged("EquipmentCategory"); }
        }

        public EquipmentCategoryEditInfoViewModel(EquipmentCategory eqcat)
        {
            _equipmentCategory = eqcat;
        }
    }
}
