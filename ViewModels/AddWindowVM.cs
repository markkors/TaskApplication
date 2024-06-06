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

namespace TaskApplication.ViewModels
{

    

    public class AddWindowVM : INotifyPropertyChanged
    {

        private string _title;
        private string _description;
        private int _category;
        private string _deadline;
        private int _finished;

        public event PropertyChangedEventHandler PropertyChanged;

        // create a method to raise the PropertyChanged event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddWindowVM()
        {
            
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
                 // add a task to the database
                 Models.task task = new Models.task();
                    task.title = title;
                    task.description = description;
                    task.category = category;
                    // prevent null values
                    //for deadline
                    if (deadline == null)
                    {
                        deadline = "";
                    }
                    task.deadline = deadline;
                    task.finished = finished;
                    App.db.InsertTask(task);

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
