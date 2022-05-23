using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    internal class ClientEditInfoViewModel:BaseViewModel
    {
        private Client _client;

        public Client Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged("Client"); }
        }

        public ClientEditInfoViewModel(Client cl)
        {
            _client = cl;
        }
    }
}
