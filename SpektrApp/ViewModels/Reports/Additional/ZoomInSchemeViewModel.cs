using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpektrApp.ViewModels.Reports.Additional
{
    internal class ZoomInSchemeViewModel:BaseViewModel
    {
        private string _selectedFilePath;

        //Путь к выбранному файлу
        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { _selectedFilePath = value; OnPropertyChanged(nameof(SelectedFilePath)); }
        }

        public ZoomInSchemeViewModel(string _path)
        {
            SelectedFilePath = _path;
        }
    }
}
