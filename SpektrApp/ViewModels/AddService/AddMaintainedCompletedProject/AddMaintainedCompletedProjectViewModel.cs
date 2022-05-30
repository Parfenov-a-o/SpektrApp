using System;
using System.Collections.Generic;
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


namespace SpektrApp.ViewModels.AddService.AddMaintainedCompletedProject
{
    internal class AddMaintainedCompletedProjectViewModel:BaseViewModel
    {
        private MaintainedObject _maintainedObject;

        private RelayCommand _addCommand;
        private RelayCommand _showSearchClientViewCommand;
        private RelayCommand _showChooseEmployeeViewCommand;
        private RelayCommand _showInstallationEquipmnetViewCommand;
        private RelayCommand _addNewClientCommand;

        private IEnumerable<Client> _clientList;

        public MaintainedObject MaintainedObject
        {
            get { return _maintainedObject; }
            set { _maintainedObject = value; OnPropertyChanged(nameof(MaintainedObject)); }
        }

        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }

        public InstallationEquipmentViewModel InstEquipVM
        {
            get; set;
        }


        public AddMaintainedCompletedProjectViewModel()
        {
            db = new ApplicationContext();

            _maintainedObject = new MaintainedObject();

            _clientList = db.Clients.ToList();

            InstEquipVM = new InstallationEquipmentViewModel();
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

                      if (view.ShowDialog() == true)
                      {
                          MaintainedObject.Client = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id);

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

                      InstallationEquipmentViewModel viewModel = new InstallationEquipmentViewModel();

                      List<InstalledEquipment> list1 = new List<InstalledEquipment>();
                      foreach (var item in MaintainedObject.InstalledEquipments)
                      {
                          list1.Add(new InstalledEquipment()
                          {
                              Id = item.Id,
                              Count = item.Count,
                              Equipment = item.Equipment,
                              EquipmentId = item.EquipmentId,
                              IndexNumber = item.IndexNumber,
                          });
                      }

                      viewModel.InstalledEquipmentList = list1;
                      InstallationEquipmentAdditionalView view = new InstallationEquipmentAdditionalView(viewModel);
                      if (view.ShowDialog() == true)
                      {
                          List<InstalledEquipment> _installedEquipmentList = new List<InstalledEquipment>();

                          foreach (var item in viewModel.InstalledEquipmentList.ToList())
                          {
                              _installedEquipmentList.Add(new InstalledEquipment()
                              {
                                  Id = item.Id,
                                  Count = item.Count,
                                  Equipment = item.Equipment,
                                  EquipmentId = item.EquipmentId,
                                  IndexNumber = item.IndexNumber,
                              });
                          }

                          MaintainedObject.InstalledEquipments = _installedEquipmentList;
                      }


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
                          MaintainedObject.Client = client;
                      }



                  }));
            }
        }

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

                          

                          //CompletedProject.Client = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id);
                      }


                  }));
            }
        }









    }
}
