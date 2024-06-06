using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskApplication.Models;
using TaskApplication.Views;

namespace TaskApplication.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private task _selectedtask;
       
       

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

        public task SelectedTask {
            get { return _selectedtask; }
            set { _selectedtask = value;
                OnPropertyChanged("SelectedTask");
            } 
        }

      

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

        public ICommand OpenAddTaskWindowCommand
        {
            get
            {
                return new Models.RelayCommand((param) =>
                {
                    // Open a new window to add a task
                    AddTask addTaskWindow = new AddTask();
                    AddWindowVM dt = (AddWindowVM)addTaskWindow.DataContext;
                    dt.Action = AddWindowVM.action.add;

                    addTaskWindow.Closing += (sender, e) =>
                    {
                        // update the tasks collection
                        Tasks = App.db.GetTasks();
                        OnPropertyChanged("Tasks");
                    };
                    
                    addTaskWindow.ShowDialog();
                        
                        
                    
                    // this will add a task to the tasks collection
                }, (param) => true);
            }
        }

        public ICommand OpenEditTaskWindowCommand
        {
            get
            {
                return new Models.RelayCommand((param) =>
                {
                    // Open a new window to add a task
                    AddTask editTaskWindow = new AddTask();
                    AddWindowVM dt = (AddWindowVM)editTaskWindow.DataContext;
                    task T = (task)param;
                    if(T == null)
                    {
                        MessageBox.Show("Please select a task to edit");
                        return;
                    }
                    dt.Task = T;
                    dt.Action = AddWindowVM.action.update;

                    editTaskWindow.Closing += (sender, e) =>
                    {
                        // update the tasks collection
                        Tasks = App.db.GetTasks();
                        OnPropertyChanged("Tasks");
                    };

                    editTaskWindow.ShowDialog();



                    // this will add a task to the tasks collection
                }, (param) => true);
            }
        }





    }
}
