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
    //ViewModel для окна "Справочник оборудования"
    internal class EquipmentViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand filterByCategoryCommand;
        private IEnumerable<Equipment> _equipmentList;
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private Equipment _selectedEquipment;

        //Список оборудования
        public IEnumerable<Equipment> EquipmentList
        {
            get { return _equipmentList; }
            set { _equipmentList = value; OnPropertyChanged("EquipmentList"); }
        }
        //Список категорий оборудования
        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged("EquipmentCategoryList"); }
        }
        //Выбранное из списка оборудование
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; OnPropertyChanged("SelectedEquipment"); }
        }

        //Конструктор без параметров
        public EquipmentViewModel()
        {
            db = new ApplicationContext();

            db.Equipments.ToList();
            db.EquipmentCategories.ToList();

            _equipmentList = db.Equipments.Local.ToBindingList();
            _equipmentCategoryList = db.EquipmentCategories.Local.ToBindingList();
        }


        //Команда для добавления нового оборудования
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


        //Команда для редактирования информации о выбранном оборудовании
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

                      //Копируем данные о выбранном оборудовании в новый ViewModel
                      EquipmentEditInfoViewModel equipvm = new EquipmentEditInfoViewModel(new Equipment()
                      {
                          Id = equipment.Id,
                          Description = equipment.Description,
                          Name = equipment.Name,
                          EquipmentCategory = equipment.EquipmentCategory,
                          EquipmentCategoryId = equipment.EquipmentCategoryId,
                          Units = equipment.Units,
                      });

                      //Создаем представление
                      EquipmentEditInfoView view = new EquipmentEditInfoView(equipvm);

                      //Вызов диалогового окна
                      if (view.ShowDialog() == true)
                      {
                          //Извлекаем объект из БД по соответствующему id
                          equipment = db.Equipments.Find((object)equipvm.Equipment.Id);
                          //Вносим изменения в объект и сохраняем в БД
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

        //Команда для фильтрации оборудования по категории
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
                          return;
                      }

                      EquipmentCategory equipmentCategory = selectedItem as EquipmentCategory;

                      //Извлечь список оборудования выбранной категории
                      EquipmentList = db.Equipments.Where(u=>u.EquipmentCategoryId == equipmentCategory.Id).ToList();


                  }));
            }
        }


    }
}
