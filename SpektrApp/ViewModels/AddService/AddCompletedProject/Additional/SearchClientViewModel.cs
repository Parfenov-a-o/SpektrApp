using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService.AddCompletedProject.Additional
{
    //ViewModel для окна "Поиск клиента"
    internal class SearchClientViewModel:BaseViewModel
    {
        private RelayCommand _chooseClientCommand;
        private RelayCommand _searchClientCommand;
        private string _searchingClientName;
        private List<Client> _clientList;
        private List<Client> _allClientList;
        private Client _selectedClient;

        //Введенное в строку поиска значение
        public string SearchingClientName
        {
            get { return _searchingClientName; }
            set { _searchingClientName = value; OnPropertyChanged(nameof(SearchingClientName)); }
        }

        //Список найденных клиентов
        public List<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged(nameof(ClientList)); }
        }

        //Список доступных для поиска клиентов
        public List<Client> AllClientList
        {
            get { return _allClientList; }
            set { _allClientList = value; OnPropertyChanged(nameof(AllClientList)); }
        }
        //Выбранный клиент
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        //Конструктор без параметров
        public SearchClientViewModel()
        {
            using(var db = new ApplicationContext())
            {
                _clientList = new List<Client>();
                _allClientList = new List<Client>();
                foreach(var client in db.Clients)
                {
                    ClientList.Add(client);
                    AllClientList.Add(client);
                }
                _searchingClientName = "";
            }
            

        }

        



        //Команда для поиска
        public RelayCommand SearchClientCommand
        {
            get
            {
                return _searchClientCommand ??
                  (_searchClientCommand = new RelayCommand((o) =>
                  {
                      //Очистить результаты предыдущего поиска
                      ClientList.Clear();
                      if(SearchingClientName == "")
                      {
                          //Выбрать всех из списка "Все клиенты"
                          ClientList = AllClientList.Select(c=>c).ToList();
                      }
                      else
                      {
                          //Выбрать из списка "Все клиенты" по совпадению с указанным значением в строке поиска
                          ClientList = AllClientList.Where(c => c.Name.ToLower().Contains(SearchingClientName.ToLower())).ToList();
                      }
                  }));
            }
        }


    }
}
