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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace Process1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int num = 0;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Start();
            dispatcherTimer.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,1);
            dispatcherTimer.Start();
        }

        private void Start()
        {
            dispatcherTimer.Stop();
            List_box.Items.Clear();
            var processe = System.Diagnostics.Process.GetProcesses();
            num = 0;
            var process1 = processe.OrderBy(x => x.ProcessName);
            List_box.Items.Add(new Process.Task("Procec Name", "Threads Count", "Base Priority"));
            foreach (var proc in process1)
            {
               

                List_box.Items.Add(new Process.Task(proc.ProcessName, proc.Threads.Count.ToString(), proc.BasePriority.ToString()));
/*               MessageBox.Show(proc.HandleCount.ToString()*/
               
                num += proc.Threads.Count;
                //if (proc.ProcessName == "notepad") proc.Kill();
            }
            Full_Count.Content = num.ToString();
        }

        private void List_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dispatcherTimer.Stop();
        }



        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            Close();
        }
    }
}
