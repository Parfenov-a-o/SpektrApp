using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.Models
{
    //Модель "Роль пользователя"
    internal class UserRole:BaseModel
    {
        private string? _name;

        //Идентификатор роли
        public int Id { get; set; }
        //Название роли
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        //Список пользователей
        public List<User> Users { get; set; } = new();

    }
}
