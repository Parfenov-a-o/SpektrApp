using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;
using SpektrApp.Views.AddService.AddCompletedProject.Additional;
using SpektrApp.ViewModels.AddService.AddCompletedProject.Additional;
using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject;
using SpektrApp.ViewModels.Handbook.EditInformationViewModels;
using SpektrApp.Views.Handbook.EditInfoViews;
using SpektrApp.Views.AddService.AddMaintainedCompletedProject;
using Microsoft.Win32;
using System.IO;
using SpektrApp.Views.AddService.AddCompletedProject;
using Microsoft.EntityFrameworkCore;
using SpektrApp.Views.Reports.Additional;

namespace SpektrApp.ViewModels.AddService
{
    //ViewModel для окна "Добавить монтажный проект"
    internal class AddCompletedProjectViewModel:BaseViewModel
    {
        //Завершенный проект со всеми сведениями
        private CompletedProject _completedProject;
        
        private RelayCommand _addCommand;
        private RelayCommand _showSearchClientViewCommand;
        private RelayCommand _showChooseEmployeesViewCommand;
        private RelayCommand _showInstallationEquipmnetViewCommand;
        private RelayCommand _addNewClientCommand;
        private RelayCommand _addSchemeFileCommand;
        private RelayCommand _removeFileCommand;
        private RelayCommand _saveChangesCommand;

        private IEnumerable<Client> _clientList;
        private Client _selectedClient;
        private DateTime _selectedDate;
        private string _address;
        private string _objectDescription;
        private string _selectedFilePath;
        private string _shortSelectedFilePath;
        private int _idSelectedCompletedProject;

        bool _isAgreeService;

        //Список выбранных сотрудников
        public ObservableCollection<Employee> SelectedEmployees { get; set; } = new ObservableCollection<Employee>();
        //Список выбранного оборудования
        public ObservableCollection<InstalledEquipment> SelectedEquipment { get; set; } = new ObservableCollection<InstalledEquipment>();
        
        //Завершенный проект
        public CompletedProject CompletedProject
        {
            get { return _completedProject; }
            set { _completedProject = value; OnPropertyChanged(nameof(CompletedProject)); }
        }
        //Список клиентов
        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }

        //ViewModel для окна "Выбрать установленное оборудование"
        public InstallationEquipmentViewModel InstEquipVM
        {
            get;set;
        }
        //ViewModel для окна "Выбрать сотрудников, выполнивших монтажный проект"
        public ChooseEmployeesViewModel ChooseEmployeesVM
        {
            get;set;
        }

        //Выбранный клиент
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        //Выбрано ли также заполнение информации об обслуживании клиента
        public bool IsAgreeService
        {
            get { return _isAgreeService; }
            set { _isAgreeService = value; OnPropertyChanged(nameof(IsAgreeService));}
        }

        //Выбранная дата
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }

        //Адрес объекта
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }
        //Дополнительное описание объекта
        public string ObjectDescription
        {
            get { return _objectDescription; }
            set { _objectDescription = value; OnPropertyChanged(nameof(ObjectDescription)); }
        }
        //Путь к выбранному файлу
        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { _selectedFilePath = value; OnPropertyChanged(nameof(SelectedFilePath)); }
        }
        //Название выбранного файла
        public string ShortSelectedFilePath
        {
            get { return _shortSelectedFilePath; }
            set { _shortSelectedFilePath = value; OnPropertyChanged(nameof(ShortSelectedFilePath)); }
        }
        //Идентификатор выбранного монтажного проекта
        public int IdSelectedCompletedProject
        {
            get { return _idSelectedCompletedProject; }
            set { _idSelectedCompletedProject = value; OnPropertyChanged(nameof(IdSelectedCompletedProject));}
        }


        //Конструктор без параметров
        public AddCompletedProjectViewModel()
        {
            using(db = new ApplicationContext())
            {
                //Загрузка списка клиентов из БД
                _clientList = db.Clients.ToList();

            }

            //Создание ViewModel для окон "Выбрать установленное оборудование" и "Выбрать сотрудников, выполнивших монтажный проект"
            InstEquipVM = new InstallationEquipmentViewModel();
            ChooseEmployeesVM = new ChooseEmployeesViewModel();

            //Установка сегодняшней даты
            SelectedDate = DateTime.Now.Date;

        }



        //Команда для добавления выполненного монтажного проекта в БД
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand((o) =>
                  {
                      using (db = new ApplicationContext())
                      {
                          
                          db.Clients.ToList();
                          db.Equipments.ToList();
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();
                          db.EquipmentCategories.ToList();

                          CompletedProject = new CompletedProject();

                          //Установка адреса объекта
                          CompletedProject.Address = Address;
                          //Установка дополнительного описания
                          CompletedProject.ObjectDescription = ObjectDescription;


                          //Установка выбранной даты
                          CompletedProject.ProjectCompletionDate = SelectedDate;

                          //Поиск клиента в таблице БД по соответствующему ключу
                          CompletedProject.Client = db.Clients.Find(SelectedClient?.Id);

                          //Добавление выбранного оборудования к объекту типа "CompletedProject"
                          foreach (var equipment in SelectedEquipment.ToList())
                          {
                              CompletedProject.InstalledEquipments.Add(new InstalledEquipment() 
                              {
                                  Count = equipment.Count,
                                  Equipment = db.Equipments.Find(equipment.Equipment?.Id),
                                  IndexNumber = equipment.IndexNumber,
                                  EquipmentId = equipment.EquipmentId,
                              });
                          }

                          //Добавление выбранных сотрудников к объекту типа "CompletedProject"
                          foreach (var employee in SelectedEmployees)
                          {
                              CompletedProject.Employees.Add(db.Employees.Find(employee.Id));
                          }

                          //Добавление файла схемы проекта в БД
                          if(SelectedFilePath != null)
                          {
                              
                              FileInfo fileInf = new FileInfo(SelectedFilePath);
                              if (fileInf.Exists)
                              {
                                  string _newFilePath = Directory.GetCurrentDirectory() + "/Resources/Images/Schemes/" + ShortSelectedFilePath;
                                  fileInf.CopyTo(_newFilePath, true);
                                  CompletedProject.ObjectSchema = _newFilePath ;
                              }
                              else
                              {
                                  MessageBox.Show("Не удалось найти файл по выбранному пути!");
                              }

                          }

                          //Валидация модели проекта
                          string? _message = "";

                          if (CompletedProject.ProjectCompletionDate.HasValue == false)
                          {
                              _message += "\nВы не указали дату завершения монтажного проекта!";
                          }

                          if (CompletedProject.Client == null)
                          {
                              _message += "\nВы не выбрали клиента!";

                          }

                          if (CompletedProject.InstalledEquipments.Count() == 0)
                          {
                              _message += "\nВы не выбрали установленное оборудование!";


                          }

                          if (CompletedProject.Employees.Count() == 0)
                          {
                              _message += "\nВы не выбрали сотрудников!";

                          }


                          if (_message.Length > 0)
                          {
                              MessageBox.Show(_message);
                              return;
                          }


                          //Попытка добавить "Выполненный монтажный проект" в БД
                          try
                          {
                              db.CompletedProjects.Add(CompletedProject);
                              db.SaveChanges();
                              
                              //Если пользователь поставил галочку в чекбоксе "Заполнить сведения об обслуживании"
                              if (IsAgreeService)
                              {
                                  MessageBox.Show("Проект успешно добавлен! Заполните сведения об обслужвании.");

                                  AddMaintainedCompletedProjectViewModel viewModel = new AddMaintainedCompletedProjectViewModel();

                                  //Передача заполненных полей во ViewModel окна "Добавить сведения о техническом обслуживании"
                                  viewModel.SelectedStartDate = SelectedDate;
                                  viewModel.SelectedEndDate = SelectedDate.AddYears(1);
                                  viewModel.SelectedClient = viewModel.ClientList.First(c => c.Id == SelectedClient?.Id);
                                  viewModel.IsFromAddCompletedProjectView = true;
                                  viewModel.Address = Address;
                                  viewModel.ObjectDescription = ObjectDescription;
                                  viewModel.ShortSelectedFilePath = ShortSelectedFilePath;
                                  viewModel.SelectedFilePath = SelectedFilePath;
                                  
                                  foreach (var equip in SelectedEquipment)
                                  {
                                      viewModel.SelectedEquipment.Add(equip);
                                  }

                                  AddMaintainedCompletedProjectView view = new AddMaintainedCompletedProjectView(viewModel);


                                  if (view.ShowDialog() == true)
                                  {

                                  }
                              }
                              else
                              {
                                  MessageBox.Show("Проект успешно добавлен!");
                              }

                              //Очистить свойства после добавления
                              SelectedEmployees.Clear();
                              SelectedEquipment.Clear();
                              Address = null;
                              ObjectDescription = null;
                              IsAgreeService = false;
                              SelectedFilePath = null;
                              ShortSelectedFilePath = null;
                          }
                          catch
                          {
                              MessageBox.Show("Не удалось добавить проект в БД!");
                              return;
                          }


                          
                      }
                      
                  }));
            }
        }

        //Команда для перехода к окну "Поиск клиента"
        public RelayCommand ShowSearchClientViewCommand
        {
            get
            {
                return _showSearchClientViewCommand ??
                  (_showSearchClientViewCommand = new RelayCommand((o) =>
                  {

                      SearchClientViewModel viewModel = new SearchClientViewModel();

                      SearchClientAdditionalView view = new SearchClientAdditionalView(viewModel);
                      
                      //Если результатом закрытия окна является "true"
                      if(view.ShowDialog()==true)
                      {
                          //Выбираем найденного клиента
                          SelectedClient = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id); 
                      }
                  }));
            }
        }

        //Команда для перехода к окну "Выбрать сотрудников, выполнивших монтажный проект"
        public RelayCommand ShowChooseEmployeesViewCommand
        {
            get
            {
                return _showChooseEmployeesViewCommand ??
                  (_showChooseEmployeesViewCommand = new RelayCommand((_) =>
                  {
                      ChooseEmployeesViewModel viewModel = new ChooseEmployeesViewModel();


                      using (db = new ApplicationContext())
                      {
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();
                          
                          //Перебираем всех сотрудников в БД
                          foreach(var employee in db.Employees)
                          {
                              //Заполняем список доступных для поиска сотрудников
                              viewModel.AllEmployeeList.Add(employee);
                              //Заполняем список найденных сотрудников
                              viewModel.AvailableEmployeeList.Add(employee);
                          }
                          //Если уже существуют выбранные сотрудники
                          if(SelectedEmployees.Count>0)
                          {
                              //Перебираем всех выбранных сотрудников
                              foreach (var employee in SelectedEmployees)
                              {
                                  //Заполняем список выбранных сотрудников во VM
                                  viewModel.SelectedEmployeeList.Add(employee);
                                  //Удаляем из списка доступных для поиска сотрудников выбранных сотрудников
                                  viewModel.AllEmployeeList.Remove(viewModel.AllEmployeeList.First(empl => empl.Id == employee.Id));
                                  //Удаляем из списка найденных сотрудников выбранных сотрудников
                                  viewModel.AvailableEmployeeList.Remove(viewModel.AvailableEmployeeList.First(empl => empl.Id == employee.Id));
                              }
                          }

                          //Создаем соответствующее представление
                          ChoiceEmployeeInstallationAdditionalView view = new ChoiceEmployeeInstallationAdditionalView(viewModel);

                          //Если закрытие диалогового окна завершилось успешно
                          if (view.ShowDialog() == true)
                          {
                              //Очищаем предыдущий список выбранных сотрудников
                              SelectedEmployees.Clear();
                              //Перебираем выбранных сотрудников из VM
                              foreach(var employee in viewModel.SelectedEmployeeList)
                              {
                                  //Заполняем список выбранных сотрудников
                                  SelectedEmployees.Add(db.Employees.First(empl => empl.Id == employee.Id));
                              }

                          }

                      }

                  }));
            }
        }

        //Команда для перехода к окну "Выбрать установленное оборудование"
        public RelayCommand ShowInstallationEquipmnetViewCommand
        {
            get
            {
                return _showInstallationEquipmnetViewCommand ??
                  (_showInstallationEquipmnetViewCommand = new RelayCommand((_) =>
                  {
                      //Создание копии коллекции
                      ObservableCollection<InstalledEquipment> list1 = new ObservableCollection<InstalledEquipment>();
                      //Заполняем список ранее выбранным оборудованием
                      foreach (var item in SelectedEquipment)
                      {
                          list1.Add(new InstalledEquipment()
                          {
                              Count = item.Count,
                              Equipment = item.Equipment,
                              EquipmentId = item.EquipmentId,
                              IndexNumber = item.IndexNumber,
                          });
                      }

                      //Заполняем список во VM
                      InstEquipVM.InstalledEquipmentList = list1;


                      using (db = new ApplicationContext())
                      {
                          db.Equipments.ToList();
                          db.EquipmentCategories.ToList();

                          //Создаём соответствующее представление
                          InstallationEquipmentAdditionalView view = new InstallationEquipmentAdditionalView(InstEquipVM);

                          //Если результат закрытия диалогового окна "true"
                          if (view.ShowDialog() == true)
                          {
                              //Очищаем список ранее выбранного оборудования
                              SelectedEquipment.Clear();

                              //Добавление выбранного оборудования в коллекцию
                              foreach (var item in InstEquipVM.InstalledEquipmentList.ToList())
                              {
                                  SelectedEquipment.Add(new InstalledEquipment()
                                  {
                                      Count = item.Count,
                                      Equipment = item.Equipment,
                                      EquipmentId = item.EquipmentId,
                                      IndexNumber = item.IndexNumber,
                                  });

                              }
                          }
                      }


                  }));
            }
        }

        //Команда для добавления нового клиента
        public RelayCommand AddNewClientCommand
        {
            get
            {
                return _addNewClientCommand ??
                  (_addNewClientCommand = new RelayCommand((o) =>
                  {
                      ClientEditInfoViewModel clvm = new ClientEditInfoViewModel(new Client());

                      ClientEditInfoView view = new ClientEditInfoView(clvm);

                      using (var db = new ApplicationContext())
                      {


                          if (view.ShowDialog() == true)
                          {
                              try
                              {
                                  //Получить клиента из VM
                                  Client client = clvm.Client;
                                  //Добавить клиента в таблицу "Клиенты"
                                  db.Clients.Add(client);
                                  //Сохранить изменения в БД
                                  db.SaveChanges();

                                  //Обновить список клиентов
                                  ClientList = db.Clients.ToList();
                                  //Назначить добавленного клиента в качестве выбранного
                                  SelectedClient = client;
                              }
                              catch
                              {
                                  //Вывести сообщение об ошибке
                                  MessageBox.Show("Ошибка! Не удалось добавить нового клиента!");
                              }
                              
                          }
                      }





                  }));
            }
        }

        //Команда для прикрепления файла-чертежа
        public RelayCommand AddSchemeFileCommand
        {
            get
            {
                return _addSchemeFileCommand ??
                  (_addSchemeFileCommand = new RelayCommand((o) =>
                  {
                      //Создать объект типа "OpenFileDialog" для создания диалогового окна
                      OpenFileDialog openFileDialog = new OpenFileDialog();
                      //Установить фильтры файлов (Возможные форматы файлов для добавления)
                      openFileDialog.Filter = "Файлы изображений (*.jpg,*.png,*.bmp,*.jpeg)| *.jpg;*.png;*.bmp;*.jpeg";
                      if (openFileDialog.ShowDialog() == true)
                      {
                          //Извлечение пути к выбранному файлу
                          SelectedFilePath = openFileDialog.FileName;
                          //Извлечение названия файла из пути
                          ShortSelectedFilePath = SelectedFilePath.Substring(SelectedFilePath.LastIndexOf('\\') + 1);
                      }

                  }));
            }
        }

        //Команда для удаления прикрепленного файла-чертежа
        public RelayCommand RemoveFileCommand
        {
            get
            {
                return _removeFileCommand ??
                  (_removeFileCommand = new RelayCommand((o) =>
                  {
                      //Очистить соответствующие свойства
                      ShortSelectedFilePath = null;
                      SelectedFilePath = null;
                      
                  }));
            }
        }

        //Команда для сохранения изменений
        public RelayCommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ??
                  (_saveChangesCommand = new RelayCommand((o) =>
                  {
                      using (db = new ApplicationContext())
                      {
                          db.Clients.ToList();
                          db.Equipments.ToList();
                          db.Employees.ToList();
                          db.EmployeePositions.ToList();
                          db.EquipmentCategories.ToList();
                          db.CompletedProjects.ToList();

                          CompletedProject _editableCompletedProject = db.CompletedProjects.First(prj=>prj.Id==IdSelectedCompletedProject);
                          string _oldFilePath = _editableCompletedProject.ObjectSchema;
                          db.CompletedProjects.Remove(_editableCompletedProject);
                          db.SaveChanges();
                          if (_editableCompletedProject != null)
                          {
                              _editableCompletedProject = new CompletedProject();

                              _editableCompletedProject.Id = IdSelectedCompletedProject;

                              //Установка адреса объекта
                              _editableCompletedProject.Address = Address;
                              //Установка дополнительного описания
                              _editableCompletedProject.ObjectDescription = ObjectDescription;


                              //Установка выбранной даты
                              _editableCompletedProject.ProjectCompletionDate = SelectedDate;

                              //Поиск клиента в таблице БД по соответствующему ключу
                              _editableCompletedProject.Client = db.Clients.Find(SelectedClient?.Id);

                              //Добавление выбранного оборудования к объекту типа "CompletedProject"
                              foreach (var equipment in SelectedEquipment.ToList())
                              {
                                  _editableCompletedProject.InstalledEquipments.Add(new InstalledEquipment()
                                  {
                                      Count = equipment.Count,
                                      Equipment = db.Equipments.Find(equipment.Equipment?.Id),
                                      IndexNumber = equipment.IndexNumber,
                                      EquipmentId = equipment.EquipmentId,
                                  });
                              }

                              //Добавление выбранных сотрудников к объекту типа "CompletedProject"
                              foreach (var employee in SelectedEmployees)
                              {
                                  _editableCompletedProject.Employees.Add(db.Employees.Find(employee.Id));
                              }

                              //Добавление файла схемы проекта в БД
                              if ((SelectedFilePath != null) &&( SelectedFilePath!= _oldFilePath))
                              {

                                  FileInfo fileInf = new FileInfo(SelectedFilePath);
                                  if (fileInf.Exists)
                                  {
                                      string _newFilePath = Directory.GetCurrentDirectory() + "/Resources/Images/Schemes/" + ShortSelectedFilePath;
                                      fileInf.CopyTo(_newFilePath, true);
                                      _editableCompletedProject.ObjectSchema = _newFilePath;
                                  }
                                  else
                                  {
                                      MessageBox.Show("Не удалось найти файл по выбранному пути!");
                                  }

                              }
                              else if(SelectedFilePath == _oldFilePath)
                              {
                                  _editableCompletedProject.ObjectSchema = _oldFilePath;
                              }


                              //Валидация модели проекта
                              string? _message = "";

                              if (_editableCompletedProject.ProjectCompletionDate.HasValue == false)
                              {
                                  _message += "\nВы не указали дату завершения монтажного проекта!";
                              }

                              if (_editableCompletedProject.Client == null)
                              {
                                  _message += "\nВы не выбрали клиента!";

                              }

                              if (_editableCompletedProject.InstalledEquipments.Count() == 0)
                              {
                                  _message += "\nВы не выбрали установленное оборудование!";


                              }

                              if (_editableCompletedProject.Employees.Count() == 0)
                              {
                                  _message += "\nВы не выбрали сотрудников!";

                              }


                              if (_message.Length > 0)
                              {
                                  MessageBox.Show(_message);
                                  return;
                              }

                              try 
                              {
                                  db.CompletedProjects.Add(_editableCompletedProject);
                                  db.SaveChanges();
                                  MessageBox.Show("Изменения внесены успешно!");

                                  foreach (Window window in App.Current.Windows)
                                  {
                                      if (window is EditInfoAboutCompletedProjectView)
                                      {
                                          window.DialogResult = true;
                                      }
                                  }
                              }
                              catch (Exception ex)
                              {
                                  MessageBox.Show("Произошла ошибка при попытке изменения данных!");
                              }

                          }
                      }

                  }));
            }
        }
    }
}
