using SpektrApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.ViewModels.InfoUser.Additional
{
    internal class UserEditInfoViewModel:BaseViewModel
    {
        private User _user;
        List<UserRole> _userRoleList;
        UserRole _selectedUserRole;

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(nameof(User)); }
        }

        public List<UserRole> UserRoleList
        {
            get { return _userRoleList; }
            set { _userRoleList = value; OnPropertyChanged(nameof(UserRoleList)); }
        }

        public UserRole SelectedUserRole
        {
            get { return _selectedUserRole; }
            set { _selectedUserRole = value; OnPropertyChanged(nameof(SelectedUserRole)); }
        }

        public UserEditInfoViewModel(User u)
        {
            _user = u;

            using (db = new ApplicationContext())
            {
                _userRoleList = db.UserRoles.ToList();
                if(u.Login != null)
                {
                    _selectedUserRole = UserRoleList.Find(r => r.Id == u.UserRole.Id);
                }
                OnPropertyChanged(nameof(UserRoleList));

            }
        }

    }
}
