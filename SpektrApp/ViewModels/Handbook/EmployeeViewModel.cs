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
    //ViewModel для окна "Справочник сотрудников"
    internal class EmployeeViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private IEnumerable<Employee> _employeeList;
        private IEnumerable<EmployeePosition> _employeePositionList;
        private Employee? _selectedEmployee;

        //Список сотрудников
        public IEnumerable<Employee> EmployeeList
        {
            get { return _employeeList; }
            set { _employeeList = value; OnPropertyChanged("EmployeeList"); }
        }
        //Список должностей сотрудников
        public IEnumerable<EmployeePosition> EmployeePositionList
        {
            get { return _employeePositionList; }
            set { _employeePositionList = value; OnPropertyChanged(nameof(EmployeePositionList)); }
        }
        //Выбранный сотрудник
        public Employee? SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }
        //Конструктор без параметров
        public EmployeeViewModel()
        {
            db = new ApplicationContext();

            db.Employees.ToList();
            db.EmployeePositions.ToList();

            _employeeList = db.Employees.Local.ToBindingList();
            _employeePositionList = db.EmployeePositions.Local.ToBindingList();
        }



        //Команда для добавления нового сотрудника
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {


                      EmployeeEditInfoViewModel emplvm = new EmployeeEditInfoViewModel(new Employee());

                      EmployeeEditInfoView view = new EmployeeEditInfoView(emplvm);

                      if (view.ShowDialog() == true)
                      {
                          Employee employee = emplvm.Employee;
                          employee.EmployeePositionId = emplvm.SelectedEmployeePosition.Id;
                          db.Employees.Add(employee);
                          db.SaveChanges();
                      }


                  }));
            }
        }


        //Команда для редактирования информации о выбранном сотруднике
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
                      Employee employee = selectedItem as Employee;

                      //Копируем данные в новый VM
                      EmployeeEditInfoViewModel emplvm = new EmployeeEditInfoViewModel(new Employee()
                      {
                          Id = employee.Id,
                          Surname = employee.Surname,
                          FirstName = employee.FirstName,
                          DateOfBirth = employee.DateOfBirth,
                          EmployeePosition = employee.EmployeePosition,
                          Gender = employee.Gender,
                          EmployeePositionId = employee.EmployeePositionId,
                          Patronymic = employee.Patronymic,
                          CompletedProjects = employee.CompletedProjects,
                          Email = employee.Email,
                          MaintainedObjects = employee.MaintainedObjects,
                          PhoneNumber = employee.PhoneNumber,

                      });

                      //Создаём соответствующее представление
                      EmployeeEditInfoView view = new EmployeeEditInfoView(emplvm);

                      //Запуск диалогового окна
                      if (view.ShowDialog() == true)
                      {
                          //Извлекаем из БД объект с соответствующим id
                          employee = db.Employees.Find((object)emplvm.Employee.Id);
                          //Вносим изменения и сохраняем в БД
                          if (employee != null)
                          {
                              employee.Id = emplvm.Employee.Id;
                              employee.Surname = emplvm.Employee.Surname;
                              employee.FirstName = emplvm.Employee.FirstName;
                              employee.DateOfBirth = emplvm.Employee.DateOfBirth;
                              employee.EmployeePosition = emplvm.SelectedEmployeePosition;
                              employee.Gender = emplvm.Employee.Gender;
                              employee.EmployeePositionId = emplvm.SelectedEmployeePosition.Id;
                              employee.Patronymic = emplvm.Employee.Patronymic;
                              employee.CompletedProjects = emplvm.Employee.CompletedProjects;
                              employee.Email = emplvm.Employee.Email;
                              employee.MaintainedObjects = emplvm.Employee.MaintainedObjects;
                              employee.PhoneNumber = emplvm.Employee.PhoneNumber;

                              db.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }



    }
}
