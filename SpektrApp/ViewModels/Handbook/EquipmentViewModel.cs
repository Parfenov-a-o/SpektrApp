using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using SpektrApp.ViewModels.Handbook.EditInformationViewModels;
using SpektrApp.Views.Handbook.EditInfoViews;
using System.Windows;

namespace SpektrApp.ViewModels.Handbook
{
    internal class EquipmentViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand filterByCategoryCommand;
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


        //Команда для добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {


                      EquipmentEditInfoViewModel equipvm = new EquipmentEditInfoViewModel(new Equipment());

                      EquipmentEditInfoView view = new EquipmentEditInfoView(equipvm);

                      if (view.ShowDialog() == true)
                      {
                          Equipment equipment = equipvm.Equipment;
                          equipment.EquipmentCategoryId = equipvm.SelectedEquipmentCategory.Id;
                          db.Equipments.Add(equipment);
                          db.SaveChanges();
                      }


                  }));
            }
        }


        //Команда для редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали запись для редактирования!");
                          return;
                      }
                      // получаем выделенный объект
                      Equipment equipment = selectedItem as Equipment;

                      EquipmentEditInfoViewModel equipvm = new EquipmentEditInfoViewModel(new Equipment()
                      {
                          Id = equipment.Id,
                          Description = equipment.Description,
                          Name = equipment.Name,
                          EquipmentCategory = equipment.EquipmentCategory,
                          EquipmentCategoryId = equipment.EquipmentCategoryId,
                          Units = equipment.Units,
                      });

                      EquipmentEditInfoView view = new EquipmentEditInfoView(equipvm);

                      if (view.ShowDialog() == true)
                      {
                          equipment = db.Equipments.Find((object)equipvm.Equipment.Id);
                          if (equipment != null)
                          {
                              equipment.Id = equipvm.Equipment.Id;
                              equipment.Description = equipvm.Equipment.Description;
                              equipment.Name = equipvm.Equipment.Name;
                              equipment.EquipmentCategory = equipvm.SelectedEquipmentCategory;
                              equipment.EquipmentCategoryId = equipvm.SelectedEquipmentCategory.Id;
                              equipment.Units = equipvm.Equipment.Units;
                              
                              db.Entry(equipment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        //Команда для фильтрации по категории оборудования
        public RelayCommand FilterByCategoryCommand
        {
            get
            {
                return filterByCategoryCommand ??
                  (filterByCategoryCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          //MessageBox.Show("Вы не выбрали запись для редактирования!");
                          return;
                      }

                      EquipmentCategory equipmentCategory = selectedItem as EquipmentCategory;

                      EquipmentList = db.Equipments.Where(u=>u.EquipmentCategoryId == equipmentCategory.Id).ToList();


                  }));
            }
        }


    }
}
