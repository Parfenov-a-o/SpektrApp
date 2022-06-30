using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;
using SpektrApp.Views.AddService.AddCompletedProject.Additional;
using SpektrApp.ViewModels.AddService.AddCompletedProject.Additional;
using SpektrApp.ViewModels.Handbook.EditInformationViewModels;
using SpektrApp.Views.Handbook.EditInfoViews;


namespace SpektrApp.ViewModels.AddService
{
    internal class AddCompletedProjectViewModel:BaseViewModel
    {
        private CompletedProject _completedProject;
        
        private RelayCommand _addCommand;
        private RelayCommand _showSearchClientViewCommand;
        private RelayCommand _showChooseEmployeesViewCommand;
        private RelayCommand _showInstallationEquipmnetViewCommand;
        private RelayCommand _addNewClientCommand;

        private IEnumerable<Client> _clientList;
        
        public CompletedProject CompletedProject
        {
            get { return _completedProject; }
            set { _completedProject = value; OnPropertyChanged(nameof(CompletedProject)); }
        }

        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }

        public InstallationEquipmentViewModel InstEquipVM
        {
            get;set;
        }
        public ChooseEmployeesViewModel ChooseEmployeesVM
        {
            get;set;
        }

        public ChoiceEmployeeInstallationAdditionalView ChoiceEmployeeInstallationView
        {
            get;set;
        }



        public AddCompletedProjectViewModel()
        {
            db = new ApplicationContext();

            _completedProject = new CompletedProject();

            _clientList = db.Clients.ToList();

            InstEquipVM = new InstallationEquipmentViewModel();
            ChooseEmployeesVM = new ChooseEmployeesViewModel();
            //ChoiceEmployeeInstallationView = new ChoiceEmployeeInstallationAdditionalView(ChooseEmployeesVM);
        }



        //Команда для добавления
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand((o) =>
                  {

                      //Добавить валидацию добавления проекта
                      string? _message="";

                      if(CompletedProject.ProjectCompletionDate.HasValue==false)
                      {
                          _message += "\nВы не указали дату завершения монтажного проекта!";
                      }

                      if (CompletedProject.Client == null)
                      {
                          _message+="\nВы не выбрали клиента!";
                          
                      }

                      if (CompletedProject.InstalledEquipments.Count()==0)
                      {
                          _message += "\nВы не выбрали установленное оборудование!";
                          
                          
                      }

                      if(CompletedProject.Employees.Count()==0)
                      {
                          _message += "\nВы не выбрали сотрудников!";

                      }

                      
                      if(_message.Length>0)
                      {
                          MessageBox.Show(_message);
                          return;
                      }

                      db.CompletedProjects.Add(CompletedProject);
                      db.SaveChanges();
                      MessageBox.Show("Проект успешно добавлен!");

                      
                  }));
            }
        }


        public RelayCommand ShowSearchClientViewCommand
        {
            get
            {
                return _showSearchClientViewCommand ??
                  (_showSearchClientViewCommand = new RelayCommand((o) =>
                  {

                      SearchClientViewModel viewModel = new SearchClientViewModel();

                      SearchClientAdditionalView view = new SearchClientAdditionalView(viewModel);
                      
                      if(view.ShowDialog()==true)
                      {

                          //SelectedClient = viewModel.SelectedClient;
                          //MessageBox.Show(viewModel.SelectedClient.Name);
                          CompletedProject.Client = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id); 
                      }


                  }));
            }
        }

        public RelayCommand ShowChooseEmployeesViewCommand
        {
            get
            {
                return _showChooseEmployeesViewCommand ??
                  (_showChooseEmployeesViewCommand = new RelayCommand((o) =>
                  {

                      ChoiceEmployeeInstallationAdditionalView view = new ChoiceEmployeeInstallationAdditionalView(ChooseEmployeesVM);

                      db.Employees.ToList();
                      db.EmployeePositions.ToList();
                      ChooseEmployeesVM.SelectedEmployeeList = CompletedProject.Employees;
                      ChooseEmployeesVM.AllEmployeeList = db.Employees.Local.Except(CompletedProject.Employees);
                      ChooseEmployeesVM.AvailableEmployeeList = db.Employees.Local.Except(CompletedProject.Employees);

                      if (view.ShowDialog() == true)
                      {

                          CompletedProject.Employees = ChooseEmployeesVM.SelectedEmployeeList.ToList();

                      }



                  }));
            }
        }

        public RelayCommand ShowInstallationEquipmnetViewCommand
        {
            get
            {
                return _showInstallationEquipmnetViewCommand ??
                  (_showInstallationEquipmnetViewCommand = new RelayCommand((o) =>
                  {
                      InstallationEquipmentAdditionalView view = new InstallationEquipmentAdditionalView(InstEquipVM);

                      //db.EquipmentCategories.ToList();
                      //db.Equipments.ToList();

                      InstEquipVM.InstalledEquipmentList = CompletedProject.InstalledEquipments;

                      if (view.ShowDialog() == true)
                      {
                          CompletedProject.InstalledEquipments = InstEquipVM.InstalledEquipmentList.ToList();
                      }

                      //InstallationEquipmentViewModel viewModel = new InstallationEquipmentViewModel();

                        //List <InstalledEquipment> list1 = new List<InstalledEquipment>();
                        //foreach(var item in CompletedProject.InstalledEquipments)
                        //{
                        //    list1.Add(new InstalledEquipment()
                        //    {
                        //        Id = item.Id,
                        //        Count = item.Count,
                        //        Equipment = item.Equipment,
                        //        EquipmentId = item.EquipmentId,
                        //        IndexNumber = item.IndexNumber,
                        //    });
                        //}

                        //viewModel.InstalledEquipmentList = list1;
                        //InstallationEquipmentAdditionalView view = new InstallationEquipmentAdditionalView(viewModel);
                        //if (view.ShowDialog() == true)
                        //{
                        //    List<InstalledEquipment> _installedEquipmentList = new List<InstalledEquipment>();

                        //    foreach (var item in viewModel.InstalledEquipmentList.ToList())
                        //    {
                        //        _installedEquipmentList.Add(new InstalledEquipment()
                        //        {
                        //            Id = item.Id,
                        //            Count = item.Count,
                        //            Equipment = item.Equipment,
                        //            EquipmentId = item.EquipmentId,
                        //            IndexNumber = item.IndexNumber,
                        //        });
                        //    }

                        //    CompletedProject.InstalledEquipments = _installedEquipmentList;
                        //}


                  }));
            }
        }


        public RelayCommand AddNewClientCommand
        {
            get
            {
                return _addNewClientCommand ??
                  (_addNewClientCommand = new RelayCommand((o) =>
                  {

                      ClientEditInfoViewModel clvm = new ClientEditInfoViewModel(new Client());

                      ClientEditInfoView view = new ClientEditInfoView(clvm);

                      if (view.ShowDialog() == true)
                      {
                          Client client = clvm.Client;
                          db.Clients.Add(client);
                          db.SaveChanges();

                          ClientList = db.Clients.ToList();
                          CompletedProject.Client = client;
                      }



                  }));
            }
        }
    }
}
