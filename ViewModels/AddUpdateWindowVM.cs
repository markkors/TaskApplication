using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskApplication.Models;
using static TaskApplication.ViewModels.MainWindowVM;

namespace TaskApplication.ViewModels
{

    

    public class AddUpdateWindowVM : INotifyPropertyChanged
    {

        private string _title = "";
        private string _description = "";
        private int _category = 0;
        private string _deadline = "";
        private int _finished = 0;
        private task _task;
        private action _action;
        private string _buttontext;

        public enum action
        {
            add,
            update
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // create a method to raise the PropertyChanged event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddUpdateWindowVM()
        {

        }

        public task Task { 
            get { return _task; }
            set { 
                _task = value;
                title = _task.title;
                description = _task.description;
                category = _task.category;
                deadline = _task.deadline;
                finished = _task.finished;

                OnPropertyChanged("Task");
            } 
        }

        public action Action
        {
            get { return _action; }
            set { 
                _action = value;
                if(_action == action.add)
                {
                    ButtonText = "Add Task";
                }
                else
                {
                    ButtonText = "Update Task";
                }
            }
        }

        public string ButtonText { 
            get { return _buttontext; }
            set { 
                _buttontext = value; 
                OnPropertyChanged("ButtonText");
            } 
        }


        public string title
        {
            get { 
                return _title; 
            }
            set { 
                _title = value; 
                OnPropertyChanged("title"); 
            }
        }
        public string description { 
            get { 
                return _description; 
            } 
            set { 
                _description = value;
                OnPropertyChanged("description");
                } 
        }
        public int category { 
            get
            {
                return _category;
            }
            set { 
                _category = value;
                OnPropertyChanged("category");
            } 
        }
        public string deadline {
            get { return _deadline;  } 
            set { 
                _deadline = value;
                OnPropertyChanged("deadline");
            }
                }
        public int finished { 
            get { return _finished; }
            set { 
                _finished = value;
                OnPropertyChanged("finished");
            }
        }

        public ICommand AddTaskCommand
        {
            get
            {
                return new Models.RelayCommand((param) =>
                {
                 
                    
                    if(Action == action.add)
                    {
                        // add a task to the database
                        Models.task task = new Models.task();
                        task.title = title;
                        task.description = description;
                        task.category = category;
                        task.deadline = deadline;
                        task.finished = finished;
                        App.db.InsertTask(task);
                    }
                    else
                    {

                        // build here the update task command
                        
                        Task.title = title;
                        Task.description = description;
                        Task.category = category;
                        Task.deadline = deadline;
                        Task.finished = finished;
                        App.db.UpdateTask(Task);

                        
                    }

                    // close the window
                    Views.AddTask addTaskWindow = (Views.AddTask)param;
                    addTaskWindow.DialogResult = true;
                    addTaskWindow.Close();

                }, (param) => true);
            }
        }

        public ICommand CancelAddTaskCommand
        {
            get
            {
                return new Models.RelayCommand((param) =>
                {
                    Views.AddTask addTaskWindow = (Views.AddTask)param;
                    addTaskWindow.DialogResult = false;
                    addTaskWindow.Close();

                }, (param) => true);
            }
        }

    }
}
