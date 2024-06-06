using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskApplication.Models;

namespace TaskApplication.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        // implement the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        // create a method to raise the PropertyChanged event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowVM()
        {
            // This is the constructor of the MainWindowVM class
            Tasks = App.db.GetTasks();
        }

        public ObservableCollection<Models.task> Tasks { get; set; }

        // command to save a task to the database
        public ICommand SaveTasksCommand
        {
            get
            {
                return new Models.RelayCommand((param) =>
                {
                   App.db.SaveTasks(Tasks);
                }, (param) => true);
            }
        }
         
        

        
    }
}
