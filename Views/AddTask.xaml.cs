using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskApplication.Views
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {

        private ViewModels.AddUpdateWindowVM vm = new ViewModels.AddUpdateWindowVM();
        public AddTask()
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
