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
    internal class EmployeePositionViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;

        private IEnumerable<EmployeePosition> _employeePositionList;
        private EmployeePosition _selectedEmployeePosition;

        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged("EmployeePositionList"); }
        }
        public EmployeePosition SelectedEmployeePosition
        {
            get { return _selectedEmployeePosition; }
            set { _selectedEmployeePosition = value; OnPropertyChanged("SelectedEmployeePosition"); }
        }

        public EmployeePositionViewModel()
        {
            db = new ApplicationContext();

            db.EmployeePositions.ToList();
            _employeePositionList = db.EmployeePositions.Local.ToBindingList();
        }


        //Команда для добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {

                      EmployeePositionEditInfoViewModel emplposvm = new EmployeePositionEditInfoViewModel(new EmployeePosition());

                      EmployeePositionEditInfoView view = new EmployeePositionEditInfoView(emplposvm);

                      if(view.ShowDialog() == true)
                      {
                          EmployeePosition employeePosition = emplposvm.EmployeePosition;
                          db.EmployeePositions.Add(employeePosition);
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
                      EmployeePosition employeePosition = selectedItem as EmployeePosition;

                      EmployeePositionEditInfoViewModel emplposvm = new EmployeePositionEditInfoViewModel(new EmployeePosition()
                      {
                          Id = employeePosition.Id,
                          Name = employeePosition.Name,
                          Employees = employeePosition.Employees,
                      });

                      EmployeePositionEditInfoView view = new EmployeePositionEditInfoView(emplposvm);
                      if(view.ShowDialog()==true)
                      {
                          employeePosition = db.EmployeePositions.Find((object)emplposvm.EmployeePosition.Id);
                          if(employeePosition != null)
                          {
                              employeePosition.Id = emplposvm.EmployeePosition.Id;
                              employeePosition.Name = emplposvm.EmployeePosition.Name;
                              employeePosition.Employees = emplposvm.EmployeePosition.Employees;

                              db.Entry(employeePosition).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                              db.SaveChanges();

                          }
                      }
                      
                  }));
            }
        }
    }
}
