using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using SpektrApp.ViewModels.AddService.AddCompletedProject.Additional;
using SpektrApp.Views.AddService.AddCompletedProject.Additional;
using SpektrApp.ViewModels.Handbook.EditInformationViewModels;
using SpektrApp.Views.Handbook.EditInfoViews;
using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject.Additional;
using SpektrApp.Views.AddService.AddMaintainedCompletedProject.AdditionalMaintained;
using SpektrApp.Views.AddService.AddMaintainedCompletedProject;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using SpektrApp.Views.Reports.Additional;

namespace SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject
{
    //ViewModel для окна "Добавить обслуживаемый объект"
    internal class AddMaintainedCompletedProjectViewModel:BaseViewModel
    {
        private MaintainedObject _maintainedObject;

        private RelayCommand _addCommand;
        private RelayCommand _showSearchClientViewCommand;
        private RelayCommand _showChooseEmployeeViewCommand;
        private RelayCommand _showInstallationEquipmnetViewCommand;
        private RelayCommand _addNewClientCommand;
        private RelayCommand _addSchemeFileCommand;
        private RelayCommand _removeFileCommand;
        private RelayCommand _saveChangesCommand;

        private IEnumerable<Client> _clientList;

        Client _selectedClient;
        Employee _selectedEmployee;

        DateTime _selectedStartDate;
        DateTime _selectedEndDate;

        private string _address;
        private string _objectDescription;
        private string _selectedFilePath;
        private string _shortSelectedFilePath;
        private int _idSelectedMaintainedObject;



        //Список выбранного оборудования
        public ObservableCollection<InstalledEquipment> SelectedEquipment { get; set; } = new ObservableCollection<InstalledEquipment>();

        //Обслуживаемый объект для добавления в БД
        public MaintainedObject MaintainedObject
        {
            get { return _maintainedObject; }
            set { _maintainedObject = value; OnPropertyChanged(nameof(MaintainedObject)); }
        }

        //Список клиентов
        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }

        //Выбранная дата начала обслуживания
        public DateTime SelectedStartDate
        {
            get { return _selectedStartDate; }
            set { _selectedStartDate = value; OnPropertyChanged(nameof(SelectedStartDate)); }
        }
        //Выбранная дата окончания обслуживания
        public DateTime SelectedEndDate
        {
            get { return _selectedEndDate; }
            set { _selectedEndDate = value; OnPropertyChanged(nameof(SelectedEndDate)); }
        }
        //Выбранный клиент
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        //Выбранный сотрудник
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); }
        }
        
        //Открыто ли окно из окна "Добавить монтажный проект"
        public bool IsFromAddCompletedProjectView { get; set; }

        //Адрес объекта
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        //Дополнительное описание
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
        //Идентификатор выбранного объекта
        public int IdSelectedMaintainedObject
        {
            get { return _idSelectedMaintainedObject; }
            set { _idSelectedMaintainedObject = value; OnPropertyChanged(nameof(IdSelectedMaintainedObject));}
        }

        //ViewModel для окна "Выбрать установленное оборудование"
        public InstallationEquipmentViewModel InstEquipVM
        {
            get; set;
        }
        //ViewModel для окна "Выбрать сотрудника, ответственного за обслуживание"
        public ChooseEmployeeViewModel ChooseEmployeeVM
        {
            get; set;
        }

        //Конструктор без параметров
        public AddMaintainedCompletedProjectViewModel()
        {
            using(var db = new ApplicationContext())
            {
                _clientList = db.Clients.ToList();
            }

            InstEquipVM = new InstallationEquipmentViewModel();
            ChooseEmployeeVM = new ChooseEmployeeViewModel();

            //В качестве даты начала обслуживания по умолчанию использовать текущую дату
            SelectedStartDate = DateTime.Now.Date;
            //В качестве даты окончания обслуживания по умолчанию использовать дату спустя год от текущей даты (большинство договоров заключается сроком на год)
            SelectedEndDate = DateTime.Now.AddYears(1);
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
                      if (view.ShowDialog() == true)
                      {
                          //Выбираем найденного клиента
                          SelectedClient = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id);
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
                  (_showInstallationEquipmnetViewCommand = new RelayCommand((o) =>
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
        //Команда для перехода к окну "Выбор сотрудника, ответственного за обслуживание"
        public RelayCommand ShowChooseEmployeeViewCommand
        {
            get
            {
                return _showChooseEmployeeViewCommand ??
                  (_showChooseEmployeeViewCommand = new RelayCommand((o) =>
                  {

                      ChooseEmployeeViewModel viewModel = new ChooseEmployeeViewModel();

                      ChooseEmployeeView view = new ChooseEmployeeView(viewModel);

                      if (view.ShowDialog() == true)
                      {
                          //Выбираем найденного сотрудника
                          SelectedEmployee = viewModel.SelectedEmployee;
                      }


                  }));
            }
        }


        //Команда для добавления обслуживаемого объекта в БД
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

                          //Создание объекта типа "Обслуживаемый объект"
                          MaintainedObject = new MaintainedObject();

                          //Установка выбранных дат
                          MaintainedObject.ServiceStartDate = SelectedStartDate;
                          MaintainedObject.ServiceEndDate = SelectedEndDate;

                          //Поиск клиента в таблице БД по соответствующему ключу
                          MaintainedObject.Client = db.Clients.Find(SelectedClient?.Id);

                          //Добавление выбранного оборудования к объекту типа "MaintainedObject"
                          foreach (var equipment in SelectedEquipment.ToList())
                          {
                              MaintainedObject.InstalledEquipments.Add(new InstalledEquipment()
                              {
                                  Count = equipment.Count,
                                  Equipment = db.Equipments.Find(equipment.Equipment?.Id),
                                  IndexNumber = equipment.IndexNumber,
                                  EquipmentId = equipment.EquipmentId,
                              });
                          }

                          //Установка выбранного сотрудника
                          MaintainedObject.Employee = db.Employees.Find(SelectedEmployee?.Id);

                          //Установка адреса обслуживаемого объекта
                          MaintainedObject.Address = Address;
                          //Установка дополнительного описания
                          MaintainedObject.ObjectDescription = ObjectDescription;

                          //Добавление файла схемы проекта в БД
                          if (SelectedFilePath != null)
                          {

                              FileInfo fileInf = new FileInfo(SelectedFilePath);
                              if (fileInf.Exists)
                              {
                                  string _newFilePath = Directory.GetCurrentDirectory() + "/Resources/Images/Schemes/" + ShortSelectedFilePath;
                                  fileInf.CopyTo(_newFilePath, true);
                                  MaintainedObject.ObjectSchema = _newFilePath;
                              }
                              else
                              {
                                  MessageBox.Show("Не удалось найти файл по выбранному пути!");
                              }

                          }

                          //Валидация модели обслуживаемого объекта
                          string? _message = "";

                          if (MaintainedObject.ServiceStartDate.HasValue == false)
                          {
                              _message += "\nВы не указали дату начала обслуживания!";
                          }
                          
                          if (MaintainedObject.ServiceEndDate.HasValue == false)
                          {
                              _message += "\nВы не указали дату окончания обслуживания!";
                          }

                          if (MaintainedObject.Client == null)
                          {
                              _message += "\nВы не выбрали клиента!";

                          }

                          if (MaintainedObject.InstalledEquipments.Count() == 0)
                          {
                              _message += "\nВы не выбрали установленное оборудование!";


                          }

                          if (MaintainedObject.Employee == null)
                          {
                              _message += "\nВы не выбрали сотрудника, ответственного за обслуживание!";

                          }


                          if (_message.Length > 0)
                          {
                              MessageBox.Show(_message);
                              return;
                          }
                          //Попытка добавить "Обслуживаемый объект" в БД
                          try
                          {
                              db.MaintainedObjects.Add(MaintainedObject);
                              db.SaveChanges();
                              


                              //Очистить свойства после добавления
                              SelectedEmployee = null;
                              SelectedEquipment.Clear();
                              Address = null;
                              ObjectDescription = null;
                              SelectedFilePath = null;
                              ShortSelectedFilePath = null;

                              //Если пользователь перешёл в данное окно из окна "Добавить монтажный проект", то закрыть текущее диалоговое окно с результатом "true"
                              if(IsFromAddCompletedProjectView)
                              {
                                  foreach (Window window in App.Current.Windows)
                                  {
                                      if (window is AddMaintainedCompletedProjectView)
                                      {
                                          window.DialogResult = true;
                                      }
                                  }
                              }

                              MessageBox.Show("Обслуживаемый объект успешно добавлен!");


                          }
                          catch
                          {
                              MessageBox.Show("Не удалось добавить обслуживаемый объект в БД!");
                              return;
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

                          MaintainedObject _editableMaintainedObject = db.MaintainedObjects.First(prj => prj.Id == IdSelectedMaintainedObject);
                          string _oldFilePath = _editableMaintainedObject.ObjectSchema;
                          db.MaintainedObjects.Remove(_editableMaintainedObject);
                          db.SaveChanges();
                          if (_editableMaintainedObject != null)
                          {
                              _editableMaintainedObject = new MaintainedObject();

                              _editableMaintainedObject.Id = IdSelectedMaintainedObject;

                              //Установка адреса объекта
                              _editableMaintainedObject.Address = Address;
                              //Установка дополнительного описания
                              _editableMaintainedObject.ObjectDescription = ObjectDescription;


                              //Установка выбранной даты
                              _editableMaintainedObject.ServiceStartDate = SelectedStartDate;
                              _editableMaintainedObject.ServiceEndDate = SelectedEndDate;

                              //Поиск клиента в таблице БД по соответствующему ключу
                              _editableMaintainedObject.Client = db.Clients.Find(SelectedClient?.Id);

                              //Добавление выбранного оборудования к объекту типа "CompletedProject"
                              foreach (var equipment in SelectedEquipment.ToList())
                              {
                                  _editableMaintainedObject.InstalledEquipments.Add(new InstalledEquipment()
                                  {
                                      Count = equipment.Count,
                                      Equipment = db.Equipments.Find(equipment.Equipment?.Id),
                                      IndexNumber = equipment.IndexNumber,
                                      EquipmentId = equipment.EquipmentId,
                                  });
                              }

                              //Добавление выбранных сотрудников к объекту типа "CompletedProject"
                              _editableMaintainedObject.Employee = db.Employees.Find(SelectedEmployee.Id);

                              //Добавление файла схемы проекта в БД
                              if ((SelectedFilePath != null) && (SelectedFilePath != _oldFilePath))
                              {

                                  FileInfo fileInf = new FileInfo(SelectedFilePath);
                                  if (fileInf.Exists)
                                  {
                                      string _newFilePath = Directory.GetCurrentDirectory() + "/Resources/Images/Schemes/" + ShortSelectedFilePath;
                                      fileInf.CopyTo(_newFilePath, true);
                                      _editableMaintainedObject.ObjectSchema = _newFilePath;
                                  }
                                  else
                                  {
                                      MessageBox.Show("Не удалось найти файл по выбранному пути!");
                                  }

                              }
                              else if (SelectedFilePath == _oldFilePath)
                              {
                                  _editableMaintainedObject.ObjectSchema = _oldFilePath;
                              }


                              //Валидация модели проекта
                              string? _message = "";

                              if (_editableMaintainedObject.ServiceStartDate.HasValue == false)
                              {
                                  _message += "\nВы не указали дату начала технического обслуживания!";
                              }

                              if (_editableMaintainedObject.ServiceEndDate.HasValue == false)
                              {
                                  _message += "\nВы не указали дату завершения технического обслуживания!";
                              }

                              if (_editableMaintainedObject.Client == null)
                              {
                                  _message += "\nВы не выбрали клиента!";

                              }

                              if (_editableMaintainedObject.InstalledEquipments.Count() == 0)
                              {
                                  _message += "\nВы не выбрали установленное оборудование!";


                              }

                              if (_editableMaintainedObject.Employee == null)
                              {
                                  _message += "\nВы не выбрали сотрудника!";

                              }


                              if (_message.Length > 0)
                              {
                                  MessageBox.Show(_message);
                                  return;
                              }

                              try
                              {
                                  db.MaintainedObjects.Add(_editableMaintainedObject);
                                  db.SaveChanges();
                                  MessageBox.Show("Изменения внесены успешно!");

                                  foreach (Window window in App.Current.Windows)
                                  {
                                      if (window is EditInfoAboutMaintainedObjectView)
                                      {
                                          window.DialogResult = true;
                                      }
                                  }
                              }
                              catch
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
