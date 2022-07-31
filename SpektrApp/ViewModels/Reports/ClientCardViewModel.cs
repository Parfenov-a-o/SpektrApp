using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SpektrApp.Models;
using Microsoft.EntityFrameworkCore;
using SpektrApp.ViewModels.AddService.AddCompletedProject.Additional;
using SpektrApp.Views.AddService.AddCompletedProject.Additional;
using SpektrApp.Views.Reports.Additional;
using SpektrApp.ViewModels.AddService;
using SpektrApp.Views.AddService.AddCompletedProject;
using SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject;

namespace SpektrApp.ViewModels.Reports
{
    //ViewModel для окна "Карточки клиентов"
    internal class ClientCardViewModel : BaseViewModel
    {
        List<Client> _clientList;
        List<CompletedProject> _completedProjectList;
        List<MaintainedObject> _maintainedObjectList;
        List<CompletedProject> _allCompletedProjectList;
        List<MaintainedObject> _allMaintainedObjectList;
        List<Employee> _allEmployeeList;

        Client _selectedClient;
        CompletedProject _selectedCompletedProject;
        MaintainedObject _selectedMaintainedObject;
        Entity _selectedEntity;


        RelayCommand _filterByClientCommand;
        RelayCommand _showAdditionalInfoCommand;
        RelayCommand _showActualMaintainedObjectCommand;
        RelayCommand _showSearchClientViewCommand;
        RelayCommand _showFullSizeSchemeCommand;
        RelayCommand _editInfoAboutSelectedEntityCommand;

        bool _isCheckedShowActualCheckBox;

        //Выбранный клиент
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }
        //Выбранная сущность (Вспомогательный объект для возможности вывода подробной информации и об обслуживаемом объекте и о выполненном монтажном проекте)
        public Entity SelectedEntity
        {
            get { return _selectedEntity; }
            set { _selectedEntity = value; OnPropertyChanged(nameof(SelectedEntity)); }
        }

        //Выбранный монтажный проект
        public CompletedProject SelectedCompletedProject
        {
            get { return _selectedCompletedProject; }
            set { _selectedCompletedProject = value; OnPropertyChanged(nameof(SelectedCompletedProject)); }
        }

        //Выбранный обслуживаемый объект
        public MaintainedObject SelectedMaintainedObject
        {
            get { return _selectedMaintainedObject; }
            set { _selectedMaintainedObject = value; OnPropertyChanged(nameof(SelectedMaintainedObject)); }
        }


        //Список клиентов
        public List<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }
        //Список всех сотрудников
        public List<Employee> AllEmployeeList
        {
            get { return _allEmployeeList; }
            set { _allEmployeeList = value; OnPropertyChanged(nameof(AllEmployeeList)); }
        }

        //Список выполненных проектов у выбранного клиента
        public List<CompletedProject> CompletedProjectList
        {
            get { return _completedProjectList; }
            set { _completedProjectList = value; OnPropertyChanged(nameof(CompletedProjectList)); }
        }
        //Список всех выполненных монтажных проектов
        public List<CompletedProject> AllCompletedProjectList
        {
            get { return _allCompletedProjectList; }
            set { _allCompletedProjectList = value; OnPropertyChanged(nameof(AllCompletedProjectList)); }
        }

        //Список обслуживаемых объектов у выбранного клиента
        public List<MaintainedObject> MaintainedObjectList
        {
            get { return _maintainedObjectList; }
            set { _maintainedObjectList = value; OnPropertyChanged(nameof(MaintainedObjectList)); }
        }

        //Список всех обслуживаемых объектов
        public List<MaintainedObject> AllMaintainedObjectList
        {
            get { return _allMaintainedObjectList; }
            set { _allMaintainedObjectList = value; OnPropertyChanged(nameof(AllMaintainedObjectList)); }
        }
        //Выбран ли чекбокс "Отобразить актуальные"
        public bool IsCheckedShowActualCheckBox
        {
            get { return _isCheckedShowActualCheckBox; }
            set { _isCheckedShowActualCheckBox = value; OnPropertyChanged(nameof(IsCheckedShowActualCheckBox)); }
        }

        //Конструктор без параметров
        public ClientCardViewModel()
        {
            using (var db = new ApplicationContext())
            {
                db.Clients.ToList();
                db.CompletedProjects.ToList();
                db.Equipments.ToList();

                _clientList = db.Clients.Local.ToList();

                //Подгрузить данные о выполненных монтажных проектах и об обслуживаемых объектах
                _allCompletedProjectList = db.CompletedProjects.Include(c => c.Employees).ThenInclude(c => c.EmployeePosition).Include(c => c.InstalledEquipments).ThenInclude(e => e.Equipment.EquipmentCategory).ToList();
                _allMaintainedObjectList = db.MaintainedObjects.Include(c => c.Employee).ThenInclude(c => c.EmployeePosition).Include(c => c.InstalledEquipments).ThenInclude(e => e.Equipment.EquipmentCategory).ToList();
            }
        }

        //Команда для фильтрации по выбранному клиенту
        public RelayCommand FilterByClientCommand
        {
            get
            {
                return _filterByClientCommand ??
                  (_filterByClientCommand = new RelayCommand((selectedItem) =>
                  {

                      using (var db = new ApplicationContext())
                      {
                          db.Clients.ToList();
                          db.CompletedProjects.ToList();
                          db.Equipments.ToList();

                          _clientList = db.Clients.Local.ToList();

                          //Подгрузить данные о выполненных монтажных проектах и об обслуживаемых объектах
                          _allCompletedProjectList = db.CompletedProjects.Include(c => c.Employees).ThenInclude(c => c.EmployeePosition).Include(c => c.InstalledEquipments).ThenInclude(e => e.Equipment.EquipmentCategory).ToList();
                          _allMaintainedObjectList = db.MaintainedObjects.Include(c => c.Employee).ThenInclude(c => c.EmployeePosition).Include(c => c.InstalledEquipments).ThenInclude(e => e.Equipment.EquipmentCategory).ToList();
                      }



                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          return;
                      }

                      //Если передаваемый команде параметр является объектом типа "Client"
                      if (selectedItem is Client client)
                      {
                          //Отобразить список выполненных монтажных проектов и обслуживаемых объектов для выбранного клиента
                          SelectedEntity = null;
                          IsCheckedShowActualCheckBox = false;
                          CompletedProjectList = AllCompletedProjectList.Where(p => p.ClientId == client.Id).ToList();
                          MaintainedObjectList = AllMaintainedObjectList.Where(p => p.ClientId == client.Id).ToList();
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
                      if (view.ShowDialog() == true)
                      {
                          //Выбираем найденного клиента
                          SelectedClient = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id);

                          
                          if (SelectedClient != null)
                          {
                              //Отобразить список выполненных монтажных проектов и обслуживаемых объектов для выбранного клиента
                              SelectedEntity = null;
                              IsCheckedShowActualCheckBox = false;
                              CompletedProjectList = AllCompletedProjectList.Where(p => p.ClientId == SelectedClient.Id).ToList();
                              MaintainedObjectList = AllMaintainedObjectList.Where(p => p.ClientId == SelectedClient.Id).ToList();
                          }

                      }
                  }));
            }
        }

        //Команда для отображения дополнительной информации о выбранном монтажном проекте/обслуживаемом объекте
        public RelayCommand ShowAdditionalInfoCommand
        {
            get
            {
                return _showAdditionalInfoCommand ??
                  (_showAdditionalInfoCommand = new RelayCommand((selectedItem) =>
                  {
                      //Использование шаблонов и выражения switch для определения типа объекта
                      switch (selectedItem)
                      {
                          //Если передаваемый команде параметр является объектом типа "CompletedProject"
                          case CompletedProject project:
                              SelectedEntity = new Entity();

                              SelectedEntity.Client = SelectedClient;
                              SelectedEntity.ServiceType = "Монтажный проект";
                              SelectedEntity.Address = project.Address;
                              SelectedEntity.ObjectDescription = project.ObjectDescription;
                              SelectedEntity.EmployeeList = project.Employees;
                              SelectedEntity.EquipmentList = project.InstalledEquipments;
                              SelectedEntity.FilePath = project.ObjectSchema;
                              SelectedEntity.EndDate = project.ProjectCompletionDate.Value;
                              SelectedEntity.Id = project.Id;
                              if(project.ObjectSchema != null)
                              {
                                  SelectedEntity.HasScheme = true;
                              }

                              OnPropertyChanged(nameof(SelectedEntity));
                              break;
                          //Если передаваемый команде параметр является объектом типа "MaintainedObject"
                          case MaintainedObject maintainedObject:
                              SelectedEntity = new Entity();

                              SelectedEntity.Client = SelectedClient;
                              SelectedEntity.ServiceType = "Техническое обслуживание";
                              SelectedEntity.Address = maintainedObject.Address;
                              SelectedEntity.ObjectDescription = maintainedObject.ObjectDescription;
                              SelectedEntity.EmployeeList.Add(maintainedObject.Employee);
                              SelectedEntity.EquipmentList = maintainedObject.InstalledEquipments;
                              SelectedEntity.FilePath = maintainedObject.ObjectSchema;
                              SelectedEntity.StartDate = maintainedObject.ServiceStartDate.Value;
                              SelectedEntity.EndDate = maintainedObject.ServiceEndDate.Value;
                              SelectedEntity.Id = maintainedObject.Id;
                              if (maintainedObject.ObjectSchema != null)
                              {
                                  SelectedEntity.HasScheme = true;
                              }

                              OnPropertyChanged(nameof(SelectedEntity));
                              break;
                          case null:
                              break;

                      }

                  }));
            }
        }

        //Команда для отображения актуальных обслуживаемых объектов
        public RelayCommand ShowActualMaintainedObjectCommand
        {
            get
            {
                return _showActualMaintainedObjectCommand ??
                  (_showActualMaintainedObjectCommand = new RelayCommand((selectedItem) =>
                  {

                      //Если клиент не выбран, то вывести соответствующее сообщение
                      if(SelectedClient==null)
                      {
                          IsCheckedShowActualCheckBox = false;
                          MessageBox.Show("Вы не выбрали клиента");
                          return;
                      }
                      //Если принимаемый командой параметр является "true" и количество обслуживаемых объектов у выбранного клиента больше нуля
                      if (((selectedItem as bool?) == true) && (MaintainedObjectList.Count > 0))
                      {
                          IsCheckedShowActualCheckBox = true;
                          //Выбрать актуальные обслуживаемые объекты
                          MaintainedObjectList = AllMaintainedObjectList.Where(p => (p.ClientId == SelectedClient.Id) && (p.ServiceEndDate >= DateTime.Now.Date)).ToList();
                      }
                      //Иначе отобразить все обслуживаемые объекты у выбранного клиента
                      else if (MaintainedObjectList.Count > 0)
                      {
                          IsCheckedShowActualCheckBox = false;
                          MaintainedObjectList = AllMaintainedObjectList.Where(p => p.ClientId == SelectedClient.Id).ToList();
                      }


                  }));
            }
        }


        public RelayCommand ShowFullSizeSchemeCommand
        {
            get
            {
                return _showFullSizeSchemeCommand ??
                  (_showFullSizeSchemeCommand = new RelayCommand((o) =>
                  {
                      Additional.ZoomInSchemeViewModel viewModel = new Additional.ZoomInSchemeViewModel(SelectedEntity.FilePath);

                      ZoomInSchemeView view = new ZoomInSchemeView(viewModel);

                      view.ShowDialog();

                  }));
            }
        }
        //Изменить информацию о выбранной сущности
        public RelayCommand EditInfoAboutSelectedEntityCommand
        {
            get
            {
                return _editInfoAboutSelectedEntityCommand ??
                  (_editInfoAboutSelectedEntityCommand = new RelayCommand((o) =>
                  {
                      if(SelectedEntity!=null)
                      {
                          switch (SelectedEntity.ServiceType)
                          {
                              case "Монтажный проект":
                                  AddCompletedProjectViewModel viewModel = new AddCompletedProjectViewModel();

                                  EditInfoAboutCompletedProjectView view = new EditInfoAboutCompletedProjectView(viewModel);

                                  viewModel.SelectedClient = viewModel.ClientList.First(cl => cl.Id == SelectedClient.Id);
                                  viewModel.Address = SelectedEntity.Address;
                                  viewModel.ObjectDescription = SelectedEntity.ObjectDescription;
                                  viewModel.SelectedFilePath = SelectedEntity.FilePath;
                                  viewModel.ShortSelectedFilePath = SelectedEntity.FilePath?.Substring(SelectedEntity.FilePath.LastIndexOf('/') + 1);
                                  viewModel.SelectedDate = SelectedEntity.EndDate;
                                  viewModel.IdSelectedCompletedProject = SelectedEntity.Id;


                                  foreach (var empl in SelectedEntity.EmployeeList)
                                  {
                                      viewModel.SelectedEmployees.Add(empl);

                                  }

                                  foreach (var equip in SelectedEntity.EquipmentList)
                                  {
                                      viewModel.SelectedEquipment.Add(equip);
                                  }

                                  if (view.ShowDialog() == true)
                                  {
                                      SelectedClient = null;
                                      CompletedProjectList.Clear();
                                      MaintainedObjectList.Clear();
                                  }
                                  break;
                              case "Техническое обслуживание":
                                  AddMaintainedCompletedProjectViewModel viewModel1 = new AddMaintainedCompletedProjectViewModel();

                                  EditInfoAboutMaintainedObjectView view1 = new EditInfoAboutMaintainedObjectView(viewModel1);

                                  viewModel1.SelectedClient = viewModel1.ClientList.First(cl => cl.Id == SelectedClient.Id);
                                  viewModel1.Address = SelectedEntity.Address;
                                  viewModel1.ObjectDescription = SelectedEntity.ObjectDescription;
                                  viewModel1.SelectedFilePath = SelectedEntity.FilePath;
                                  viewModel1.ShortSelectedFilePath = SelectedEntity.FilePath?.Substring(SelectedEntity.FilePath.LastIndexOf('/') + 1);
                                  viewModel1.SelectedStartDate = SelectedEntity.StartDate;
                                  viewModel1.SelectedEndDate = SelectedEntity.EndDate;
                                  viewModel1.IdSelectedMaintainedObject = SelectedEntity.Id;
                                  viewModel1.SelectedEmployee = SelectedEntity.EmployeeList[0];

                                  foreach (var equip in SelectedEntity.EquipmentList)
                                  {
                                      viewModel1.SelectedEquipment.Add(equip);
                                  }

                                  if (view1.ShowDialog() == true)
                                  {
                                      SelectedClient = null;
                                      CompletedProjectList.Clear();
                                      MaintainedObjectList.Clear();
                                  }
                                  break;
                          }

                      }


                  }));
            }
        }


    }
    //Вспомогательный класс для хранения подробной информации о выбранном элементе
    internal class Entity
    {
        //Идентификатор сущности
        public int Id { get; set; }
        //Выбранный клиент
        public Client Client { get; set; }
        //Дата начала обслуживания
        public DateTime StartDate { get; set; }
        //Дата окончания обслуживания (+дата завершения монтажного проекта)
        public DateTime EndDate { get; set; }
        //Адрес
        public string Address { get; set; }
        //Дополнительное описание
        public string ObjectDescription { get; set; }
        //Вид предоставляемой услуги
        public string ServiceType { get; set; }
        //Путь к файлу-чертежу
        public string FilePath { get; set; }
        //Имеется ли чертеж
        public bool HasScheme { get; set; }
        //Список установленного оборудования
        public List<InstalledEquipment> EquipmentList { get; set; } = new List<InstalledEquipment>();
        //Список ответственных сотрудников
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

    }
}
