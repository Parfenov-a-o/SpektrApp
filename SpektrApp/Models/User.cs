using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Пользователь"
    internal class User : BaseModel
    {
        private string? _login;
        private string? _password;
        private int _userRoleId;
        private UserRole _userRole;


        //!!!Указать в качестве первичного ключа ЛОГИН
        //Идентификатор пользователя
        public int Id { get; set; }
        //Логин пользователя
        public string? Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
        }
        //Пароль
        public string? Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }
        //Код роли
        public int UserRoleId
        {
            get { return _userRoleId; }
            set { _userRoleId = value; OnPropertyChanged("UserRoleId"); }
        }
        //Ссылка на роль пользователя
        public UserRole? UserRole
        {
            get { return _userRole; }
            set { _userRole = value; OnPropertyChanged("UserRole"); }
        }
    }
}
