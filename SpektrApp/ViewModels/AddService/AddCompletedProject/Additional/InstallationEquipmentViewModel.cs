﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    internal class InstallationEquipmentViewModel:BaseViewModel
    {
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;
        private IEnumerable<Equipment> _availableEquipmentList;
        private Equipment _selectedEquipment;
        private double _count;
        private IEnumerable<InstalledEquipment> _installedEquipmentList;
        private RelayCommand _addInBasketCommand;

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

        public IEnumerable<Equipment> AvailableEquipmentList
        {
            get { return _availableEquipmentList; }
            set { _availableEquipmentList = value; OnPropertyChanged(nameof(AvailableEquipmentList)); }
        }
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; OnPropertyChanged(nameof(SelectedEquipment)); }
        }

        public double Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(nameof(Count)); }
        }
        public IEnumerable<InstalledEquipment> InstalledEquipmentList
        {
            get { return _installedEquipmentList; }
            set { _installedEquipmentList = value; OnPropertyChanged(nameof(InstalledEquipmentList)); }
        }

        public InstallationEquipmentViewModel()
        {
            db = new ApplicationContext();
            _equipmentCategoryList = db.EquipmentCategories.ToList();
        }



    }
}