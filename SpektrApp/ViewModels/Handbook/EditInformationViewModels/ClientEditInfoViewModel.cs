using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;

namespace SpektrApp.ViewModels.Handbook.EditInformationViewModels
{
    //ViewModel для окна "Изменить сведения о клиенте"
    internal class ClientEditInfoViewModel:BaseViewModel
    {
        private Client _client;

        //Изменяемая сущность "Клиент"
        public Client Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged("Client"); }
        }

        //Конструктор класса с параметром типа "Client"
        public ClientEditInfoViewModel(Client cl)
        {
            _client = cl;
        }
    }
}
