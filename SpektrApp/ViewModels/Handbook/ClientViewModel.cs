﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using SpektrApp.Views.Handbook.EditInfoViews;
using System.Windows;

namespace SpektrApp.ViewModels.Handbook
{
    //ViewModel для окна "Справочник клиентов"
    internal class ClientViewModel:BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        IEnumerable<Client> _clientList;

        private Client _selectedClient;

        //Список клиентов
        public IEnumerable<Client> ClientList
        {
            get { return _clientList; }
            set { _clientList = value; OnPropertyChanged("ClientList"); }
        }
        //Выбранный клиент
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }
        //Конструктор без параметров
        public ClientViewModel()
        {
            db = new ApplicationContext();

            db.Clients.ToList();
            _clientList = db.Clients.Local.ToBindingList();
        }



        //Команда для добавления нового клиента
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {


                      EditInformationViewModels.ClientEditInfoViewModel clvm = new EditInformationViewModels.ClientEditInfoViewModel(new Client());

                      ClientEditInfoView view = new ClientEditInfoView(clvm);

                      if(view.ShowDialog()==true)
                      {
                          Client client = clvm.Client;
                          db.Clients.Add(client);
                          db.SaveChanges();
                      }

                      
                  }));
            }
        }


        //Команда для редактирования информации о клиенте
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      //Если принимаемый командой параметр пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали запись для редактирования!");
                          return;
                      }
                      // получаем выделенный объект
                      Client client = selectedItem as Client;

                      //Копируем данные выбранного объекта
                      EditInformationViewModels.ClientEditInfoViewModel clvm = new EditInformationViewModels.ClientEditInfoViewModel(new Client() 
                      {
                          Id = client.Id,
                          Name = client.Name,
                          Address = client.Address,
                          CompletedProjects = client.CompletedProjects,
                          Contacts = client.Contacts,
                          Email = client.Email,
                          MaintainedObjects = client.MaintainedObjects,
                          PhoneNumber = client.PhoneNumber
                      });

                      //Создаём представление
                      ClientEditInfoView view = new ClientEditInfoView(clvm);

                      //Вызываем диалоговое окно
                      if (view.ShowDialog() == true)
                      {
                          //Находим клиента с выбранным id
                          client = db.Clients.Find((object)clvm.Client.Id);
                          //Обновляем данные
                          if(client != null)
                          {
                              client.Id = clvm.Client.Id;
                              client.Name = clvm.Client.Name;
                              client.Address = clvm.Client.Address;
                              client.CompletedProjects = clvm.Client.CompletedProjects;
                              client.Contacts = clvm.Client.Contacts;
                              client.Email = clvm.Client.Email;
                              client.MaintainedObjects = clvm.Client.MaintainedObjects;
                              client.PhoneNumber = clvm.Client.PhoneNumber;

                              db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
    }
}
