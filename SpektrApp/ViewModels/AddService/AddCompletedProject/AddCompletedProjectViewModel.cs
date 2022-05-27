using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpektrApp.Models;
using System.Windows;

namespace SpektrApp.ViewModels.AddService
{
    internal class AddCompletedProjectViewModel:BaseViewModel
    {
        private CompletedProject _completedProject;
        private RelayCommand _addCommand;

        public CompletedProject CompletedProject
        {
            get { return _completedProject; }
            set { _completedProject = value; OnPropertyChanged(nameof(CompletedProject)); }
        }

        //Команда для добавления
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand((o) =>
                  {

                      //Добавить валидацию добавления проекта

                      db.CompletedProjects.Add(CompletedProject);
                      db.SaveChanges();
                      MessageBox.Show("Проект успешно добавлен!");

                      
                  }));
            }
        }
    }
}
