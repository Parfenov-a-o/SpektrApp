using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook
{
    internal class ClientViewModel:BaseViewModel
    {
        IEnumerable<Client> _clientList;

        private Client _selectedClient;

        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged("ClientList"); }
        }

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        public ClientViewModel()
        {
            db = new ApplicationContext();

            db.Clients.ToList();
            _clientList = db.Clients.Local.ToBindingList();
        }
    }
}
