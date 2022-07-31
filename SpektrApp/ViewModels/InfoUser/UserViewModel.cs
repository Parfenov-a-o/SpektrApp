using SpektrApp.Models;
using SpektrApp.ViewModels.InfoUser.Additional;
using SpektrApp.Views.InfoUser.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpektrApp.ViewModels.InfoUser
{
    internal class UserViewModel: BaseViewModel
    {
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand _deleteCommand;


        List<User> _userList;
        User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }
        public List<User> UserList
        {
            get { return _userList; }
            set { _userList = value; OnPropertyChanged(nameof(UserList)); }
        }

        public UserViewModel()
        {
            using (db = new ApplicationContext())
            {
                db.UserRoles.ToList();
                _userList = db.Users.ToList();
            }
        }


        //Команда для добавления нового пользователя
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      UserEditInfoViewModel viewModel = new UserEditInfoViewModel(new User());

                      UserEditInfoView view = new UserEditInfoView(viewModel);

                      if (view.ShowDialog() == true)
                      {
                          try
                          {
                              using (db = new ApplicationContext())
                              {
                                  User _user = new User() 
                                  {
                                      Login = viewModel.User.Login,
                                      Password = viewModel.User.Password,
                                      UserRole = db.UserRoles.Find(viewModel.SelectedUserRole.Id),
                                  };
                                  db.Users.Add(_user);
                                  db.SaveChanges();
                                  db.UserRoles.ToList();
                                  UserList = db.Users.ToList();
                                  OnPropertyChanged(nameof(UserList));


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


        //Команда для редактирования выбранного пользователя
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
                      User user = selectedItem as User;

                      //Копируем данные в новый viewModel
                      UserEditInfoViewModel viewModel = new UserEditInfoViewModel(new User()
                      {
                          Login = user.Login,
                          Password = user.Password,
                          UserRole = user.UserRole,
                      });

                      string _oldLogin = viewModel.User.Login;

                      //Создаём представление
                      UserEditInfoView view = new UserEditInfoView(viewModel);
                      //Вызов диалогового окна
                      if (view.ShowDialog() == true)
                      {
                          using (db = new ApplicationContext())
                          {
                              //db.Users.ToList();
                              db.UserRoles.ToList();

                              //Извлекаем из БД объект с соответствующим логином
                              user = db.Users.First(u=>u.Login == _oldLogin);
                              //Вносим изменения и сохраняем их в БД
                              if (user != null)
                              {
                                  user.Login = viewModel.User.Login;
                                  user.Password = viewModel.User.Password;
                                  user.UserRole = db.UserRoles.Find(viewModel.SelectedUserRole.Id);


                                  try
                                  {
                                      db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                      db.SaveChanges();

                                      db.UserRoles.ToList();
                                      UserList = db.Users.ToList();
                                      OnPropertyChanged(nameof(UserList));
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

                              db.Users.Remove(selectedItem as User);
                              db.SaveChanges();
                              db.UserRoles.ToList();
                              UserList = db.Users.ToList();
                              OnPropertyChanged(nameof(UserList));


                          }


                      }


                  }));
            }
        }


    }
}
