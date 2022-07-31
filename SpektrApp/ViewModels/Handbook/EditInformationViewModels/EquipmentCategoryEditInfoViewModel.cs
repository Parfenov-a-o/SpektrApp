using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    //ViewModel для окна "Изменить сведения о категории оборудования"
    internal class EquipmentCategoryEditInfoViewModel:BaseViewModel
    {
        private EquipmentCategory _equipmentCategory;

        //Изменяемая сущность "Категория оборудования"
        public EquipmentCategory EquipmentCategory
        {
            get { return _equipmentCategory; }
            set { _equipmentCategory = value; OnPropertyChanged("EquipmentCategory"); }
        }
        //Конструктор с параметром типа "EquipmentCategory"
        public EquipmentCategoryEditInfoViewModel(EquipmentCategory eqcat)
        {
            _equipmentCategory = eqcat;
        }
    }
}
