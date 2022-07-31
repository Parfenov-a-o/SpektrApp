using SpektrApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.ViewModels.InfoUser.Additional
{
    internal class UserRoleEditInfoViewModel:BaseViewModel
    {
        private UserRole _userRole;

        public UserRole UserRole
        {
            get { return _userRole; }
            set { _userRole = value; OnPropertyChanged(nameof(UserRole)); }
        }

        public UserRoleEditInfoViewModel(UserRole ur)
        {
            _userRole = ur;
        }
    }
}
