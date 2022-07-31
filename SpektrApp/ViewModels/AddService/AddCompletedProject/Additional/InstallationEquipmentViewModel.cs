using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    //ViewModel для окна "Выбрать устанавливаемое оборудование"
    internal class InstallationEquipmentViewModel:BaseViewModel
    {
        private IEnumerable<EquipmentCategory> _equipmentCategoryList;
        private EquipmentCategory _selectedEquipmentCategory;
        private IEnumerable<Equipment> _availableEquipmentList;
        private IEnumerable<Equipment> _allEquipmentList;
        private Equipment _selectedEquipment;
        private double _count;
        private RelayCommand _addInBasketCommand;
        private RelayCommand _filterByCategoryCommand;

        //Список находящегося в корзине оборудования
        public ObservableCollection<InstalledEquipment> InstalledEquipmentList { get; set; } = new ObservableCollection<InstalledEquipment>();

        //Список категорий оборудования
        public IEnumerable<EquipmentCategory> EquipmentCategoryList
        {
            get { return _equipmentCategoryList; }
            set { _equipmentCategoryList = value; OnPropertyChanged(nameof(EquipmentCategoryList)); }
        }

        //Выбранная категория оборудования
        public EquipmentCategory SelectedEquipmentCategory
        {
            get { return _selectedEquipmentCategory; }
            set { _selectedEquipmentCategory = value; OnPropertyChanged(nameof(SelectedEquipmentCategory)); }
        }

        //Список доступного для выбора оборудования
        public IEnumerable<Equipment> AvailableEquipmentList
        {
            get { return _availableEquipmentList; }
            set { _availableEquipmentList = value; OnPropertyChanged(nameof(AvailableEquipmentList)); }
        }
        //Общий список оборудования
        public IEnumerable<Equipment> AllEquipmentList
        {
            get { return _allEquipmentList; }
            set { _allEquipmentList = value; OnPropertyChanged(nameof(AllEquipmentList)); }
        }
        //Выбранное оборудование
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; OnPropertyChanged(nameof(SelectedEquipment)); }
        }
        //Количество выбранного товара
        public double Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(nameof(Count)); }
        }

        //Конструктор без параметров
        public InstallationEquipmentViewModel()
        {
            using (db = new ApplicationContext())
            {
                db.EquipmentCategories.ToList();
                _equipmentCategoryList = db.EquipmentCategories.Local.ToList();
                db.Equipments.ToList();
                _allEquipmentList = db.Equipments.Local.ToList();
            }

        }

        //Команда для фильтрации оборудования по категории
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
                          return;
                      }

                      EquipmentCategory equipmentCategory = selectedItem as EquipmentCategory;

                      //Извлечь список оборудования выбранной категории
                      AvailableEquipmentList = AllEquipmentList.Where(u => u.EquipmentCategoryId == equipmentCategory.Id).ToList();


                  }));
            }
        }
        //Команда для добавления выбранного оборудования в корзину
        public RelayCommand AddInBasketCommand
        {
            get
            {
                return _addInBasketCommand ??
                  (_addInBasketCommand = new RelayCommand((selectedItem) =>
                  {

                      //Выбрано ли оборудование
                      if(SelectedEquipment==null)
                      {
                          MessageBox.Show("Вы не выбрали оборудование!");
                          return;
                      }
                      //Введенное количество товара должно быть больше нуля
                      if(Count<=0)
                      {
                          MessageBox.Show("Количество выбранного товара меньше либо равно нулю!");
                          return;
                      }

                      //Если в корзину уже добавлен выбранное оборудование
                      if (InstalledEquipmentList.Where(i => i.EquipmentId == SelectedEquipment.Id).Count() > 0)
                      {
                          //Добавляем количество
                          InstalledEquipmentList.First(i => i.EquipmentId == SelectedEquipment.Id).Count += Count;
                      }
                      else
                      {
                          //Если в корзине нет выбранного товара, то создаём новую запись
                          InstalledEquipment _installedEquipment = new InstalledEquipment()
                          {
                              IndexNumber = InstalledEquipmentList.Count() + 1,
                              Equipment = SelectedEquipment,
                              EquipmentId = SelectedEquipment.Id,
                              Count = Count,
                          };
                          InstalledEquipmentList.Add(_installedEquipment);
                      }
                      
                  }));
            }
        }




    }
}
