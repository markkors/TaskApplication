using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModels.MainWindowVM vm = new ViewModels.MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if(!vm.Saved)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save the changes?", "Save changes", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    vm.SaveTasksCommand.Execute(null);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
