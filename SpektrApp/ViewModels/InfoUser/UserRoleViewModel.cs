using SpektrApp.Models;
using SpektrApp.ViewModels.InfoUser.Additional;
using SpektrApp.Views.InfoUser.Additional;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpektrApp.ViewModels.InfoUser
{
    internal class UserRoleViewModel: BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand _deleteCommand;
        List<UserRole> _userRoleList;
        UserRole _selectedUserRole;

        //Список пользовательских ролей
        public List<UserRole> UserRoleList
        {
            get { return _userRoleList; }
            set { _userRoleList = value; OnPropertyChanged(nameof(UserRoleList)); }
        }
        //Выбранная пользовательская роль
        public UserRole SelectedUserRole
        {
            get { return _selectedUserRole; }
            set { _selectedUserRole = value; OnPropertyChanged(nameof(SelectedUserRole)); }
        }

        public UserRoleViewModel()
        {
            using(db=new ApplicationContext())
            {
                _userRoleList = db.UserRoles.ToList();
            }    
        }

        //Команда для добавления новой пользовательской роли
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {

                      UserRoleEditInfoViewModel viewModel = new UserRoleEditInfoViewModel(new UserRole());

                      UserRoleEditInfoView view = new UserRoleEditInfoView(viewModel);


                      if (view.ShowDialog() == true)
                      {
                          try
                          {
                              using (db = new ApplicationContext())
                              {
                                  UserRole _userRole = viewModel.UserRole;
                                  db.UserRoles.Add(_userRole);
                                  db.SaveChanges();
                                  UserRoleList = db.UserRoles.ToList();
                                  OnPropertyChanged(nameof(UserRoleList));


                              }

                          }
                          catch
                          {
                              MessageBox.Show("Ошибка при добавлении записи в БД!");
                          }

                      }


                  }));
            }
        }


        //Команда для редактирования выбранной пользовательской роли
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
                      UserRole userRole = selectedItem as UserRole;

                      //Копируем данные в новый viewModel
                      UserRoleEditInfoViewModel viewModel = new UserRoleEditInfoViewModel(new UserRole()
                      {
                          Id = userRole.Id,
                          Name = userRole.Name,
                          Users = userRole.Users,
                      });
                      //Создаём представление
                      UserRoleEditInfoView view = new UserRoleEditInfoView(viewModel);
                      //Вызов диалогового окна
                      if (view.ShowDialog() == true)
                      {
                          using (db = new ApplicationContext())
                          {
                              db.Users.ToList();

                              //Извлекаем из БД объект с соответствующим id
                              userRole = db.UserRoles.Find((object)viewModel.UserRole.Id);
                              //Вносим изменения и сохраняем их в БД
                              if (userRole != null)
                              {
                                  userRole.Id = viewModel.UserRole.Id;
                                  userRole.Name = viewModel.UserRole.Name;
                                  userRole.Users = viewModel.UserRole.Users;

                                  try
                                  {
                                      db.Entry(userRole).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                      db.SaveChanges();

                                      UserRoleList = db.UserRoles.ToList();
                                      OnPropertyChanged(nameof(UserRoleList));
                                  }
                                  catch
                                  {
                                      MessageBox.Show("Ошибка! Не удалось внести изменения!");
                                  }




                              }

                          }
                      }

                  }));
            }
        }


        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                  (_deleteCommand = new RelayCommand((selectedItem) =>
                  {

                      //Если параметр команды был пуст
                      if (selectedItem == null)
                      {
                          MessageBox.Show("Вы не выбрали запись для удаления!");
                          return;
                      }

                      // получаем выделенный объект
                      BaseModel employee = selectedItem as BaseModel;

                      //Создание окна подтверждения удаления
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить выбранный элемент?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                      if (result == MessageBoxResult.Yes)
                      {
                          using (db = new ApplicationContext())
                          {

                              db.UserRoles.Remove(selectedItem as UserRole);
                              db.SaveChanges();
                              UserRoleList = db.UserRoles.ToList();
                              OnPropertyChanged(nameof(UserRoleList));


                          }


                      }


                  }));
            }
        }




    }
}
