﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    internal class InstallationEquipmentViewModel:BaseViewModel
    {
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;
        private IEnumerable<Equipment> _availableEquipmentList;
        private IEnumerable<Equipment> _allEquipmentList;
        private Equipment _selectedEquipment;
        private double _count;
        private IEnumerable<InstalledEquipment> _installedEquipmentList;
        private RelayCommand _addInBasketCommand;
        private RelayCommand _filterByCategoryCommand;

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
        public IEnumerable<Equipment> AllEquipmentList
        {
            get { return _allEquipmentList; }
            set { _allEquipmentList = value; OnPropertyChanged(nameof(AllEquipmentList)); }
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
            db.EquipmentCategories.ToList();
            _equipmentCategoryList = db.EquipmentCategories.Local.ToList();
            _installedEquipmentList = new List<InstalledEquipment>();
            db.Equipments.ToList();
            _allEquipmentList = db.Equipments.Local.ToList();

        }

        //Команда для фильтрации по категории оборудования
        public RelayCommand FilterByCategoryCommand
        {
            get
            {
                return _filterByCategoryCommand ??
                  (_filterByCategoryCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          //MessageBox.Show("Вы не выбрали запись для редактирования!");
                          return;
                      }

                      EquipmentCategory equipmentCategory = selectedItem as EquipmentCategory;

                      AvailableEquipmentList = AllEquipmentList.Where(u => u.EquipmentCategoryId == equipmentCategory.Id).ToList();


                  }));
            }
        }

        public RelayCommand AddInBasketCommand
        {
            get
            {
                return _addInBasketCommand ??
                  (_addInBasketCommand = new RelayCommand((selectedItem) =>
                  {

                      if(SelectedEquipment==null)
                      {
                          MessageBox.Show("Вы не выбрали оборудование!");
                          return;
                      }

                      InstalledEquipment _installedEquipment = new InstalledEquipment()
                      {
                          IndexNumber = InstalledEquipmentList.Count() + 1,
                          Equipment = SelectedEquipment,
                          EquipmentId = SelectedEquipment.Id,
                          Count = Count,

                      };

                      if (InstalledEquipmentList.Count()==0)
                      {
                          InstalledEquipmentList = InstalledEquipmentList.Append(_installedEquipment);
                      }


                      //List<InstalledEquipment> _installedEquipmentList = InstalledEquipmentList.ToList();

                      //if(_installedEquipmentList.Where(i=>i.EquipmentId == SelectedEquipment.Id).Count()>0)
                      //{
                      //    _installedEquipmentList.Find(i => i.EquipmentId == SelectedEquipment.Id).Count += Count;
                      //}
                      //else
                      //{
                      //    InstalledEquipment _installedEquipment = new InstalledEquipment()
                      //    {
                      //        IndexNumber = InstalledEquipmentList.Count() + 1,
                      //        Equipment = SelectedEquipment,
                      //        EquipmentId = SelectedEquipment.Id,
                      //        Count = Count,

                      //    };
                      //    _installedEquipmentList.Add(_installedEquipment);
                      //}
                      //InstalledEquipmentList = _installedEquipmentList;
                  }));
            }
        }




    }
}
