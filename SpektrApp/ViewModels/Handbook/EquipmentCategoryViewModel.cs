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
    internal class EquipmentCategoryViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
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


        //Команда для добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {

                      EquipmentCategoryEditInfoViewModel equipcatvm = new EquipmentCategoryEditInfoViewModel(new EquipmentCategory());

                      EquipmentCategoryEditInfoView view = new EquipmentCategoryEditInfoView(equipcatvm);

                      if (view.ShowDialog() == true)
                      {
                          EquipmentCategory equipmentCategory = equipcatvm.EquipmentCategory;
                          db.EquipmentCategories.Add(equipmentCategory);
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
                      EquipmentCategory equipmentCategory = selectedItem as EquipmentCategory;

                      EquipmentCategoryEditInfoViewModel equipcatvm = new EquipmentCategoryEditInfoViewModel(new EquipmentCategory()
                      {
                          Id = equipmentCategory.Id,
                          Name = equipmentCategory.Name,
                          Equipments = equipmentCategory.Equipments,
                      });

                      EquipmentCategoryEditInfoView view = new EquipmentCategoryEditInfoView(equipcatvm);
                      if (view.ShowDialog() == true)
                      {
                          equipmentCategory = db.EquipmentCategories.Find((object)equipcatvm.EquipmentCategory.Id);
                          if (equipmentCategory != null)
                          {
                              equipmentCategory.Id = equipcatvm.EquipmentCategory.Id;
                              equipmentCategory.Name = equipcatvm.EquipmentCategory.Name;
                              equipmentCategory.Equipments = equipcatvm.EquipmentCategory.Equipments;

                              db.Entry(equipmentCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                              db.SaveChanges();

                          }
                      }

                  }));
            }
        }
    }
}
