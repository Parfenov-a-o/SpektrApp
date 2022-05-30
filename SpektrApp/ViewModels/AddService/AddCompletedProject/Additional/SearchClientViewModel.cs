using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    internal class SearchClientViewModel:BaseViewModel
    {
        private RelayCommand _chooseClientCommand;
        private RelayCommand _searchClientCommand;
        private string _searchingClientName;
        private IEnumerable<Client> _clientList;
        private Client _selectedClient;

        public string SearchingClientName
        {
            get { return _searchingClientName; }
            set { _searchingClientName = value; OnPropertyChanged(nameof(SearchingClientName)); }
        }

        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        public bool Result { get; set; }

        public SearchClientViewModel()
        {
            db = new ApplicationContext();
            _clientList = db.Clients.ToList();
            _searchingClientName = "";

        }

        



        //Команда для поиска
        public RelayCommand SearchClientCommand
        {
            get
            {
                return _searchClientCommand ??
                  (_searchClientCommand = new RelayCommand((o) =>
                  {
                      if(SearchingClientName == "")
                      {
                          ClientList = db.Clients.ToList();
                      }
                      else
                      {
                          ClientList = db.Clients.Where(c => c.Name.Contains(SearchingClientName)).ToList();

                      }



                  }));
            }
        }




        //Команда для редактирования
        public RelayCommand ChooseClientCommand
        {
            get
            {
                return _chooseClientCommand ??
                  (_chooseClientCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали клиента!");
                          Result = false;
                          return;
                      }
                      else
                      {
                          if(selectedItem is Client)
                          {
                              Result = true;
                          }
                      }

                      //Пока что не знаю как сделать добавление


                      //// получаем выделенный объект
                      //Client client = selectedItem as Client;

                      //EditInformationViewModels.ClientEditInfoViewModel clvm = new EditInformationViewModels.ClientEditInfoViewModel(new Client()
                      //{
                      //    Id = client.Id,
                      //    Name = client.Name,
                      //    Address = client.Address,
                      //    CompletedProjects = client.CompletedProjects,
                      //    Contacts = client.Contacts,
                      //    Email = client.Email,
                      //    MaintainedObjects = client.MaintainedObjects,
                      //    PhoneNumber = client.PhoneNumber
                      //});

                      //ClientEditInfoView view = new ClientEditInfoView(clvm);

                      //if (view.ShowDialog() == true)
                      //{
                      //    client = db.Clients.Find((object)clvm.Client.Id);
                      //    if (client != null)
                      //    {
                      //        client.Id = clvm.Client.Id;
                      //        client.Name = clvm.Client.Name;
                      //        client.Address = clvm.Client.Address;
                      //        client.CompletedProjects = clvm.Client.CompletedProjects;
                      //        client.Contacts = clvm.Client.Contacts;
                      //        client.Email = clvm.Client.Email;
                      //        client.MaintainedObjects = clvm.Client.MaintainedObjects;
                      //        client.PhoneNumber = clvm.Client.PhoneNumber;

                      //        db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                      //        db.SaveChanges();
                      //    }
                      //}
                  }));
            }
        }


    }
}
