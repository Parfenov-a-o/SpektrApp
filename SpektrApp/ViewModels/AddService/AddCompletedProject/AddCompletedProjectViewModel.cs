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

        

        public AddCompletedProjectViewModel()
        {
            db = new ApplicationContext();

            _completedProject = new CompletedProject();

            _clientList = db.Clients.ToList();
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
                      ChooseEmployeesViewModel viewModel = new ChooseEmployeesViewModel();

                      ChoiceEmployeeInstallationAdditionalView view = new ChoiceEmployeeInstallationAdditionalView();

                      if (view.ShowDialog() == true)
                      {
                          //Доделать

                          //SelectedClient = viewModel.SelectedClient;
                          //MessageBox.Show(viewModel.SelectedClient.Name);
                          //CompletedProject.Client = ClientList.ToList().Find(c => c.Id == viewModel.SelectedClient.Id);
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
                          CompletedProject.Client = client;
                      }



                  }));
            }
        }
    }
}
