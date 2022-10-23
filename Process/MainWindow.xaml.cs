﻿using System;
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
using System.Threading;

namespace Process1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int num = 0;
        Thread thread;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Start();
            dispatcherTimer.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            thread = new Thread(StarTheared);
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }
        private void StarTheared()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Start()
        {
            dispatcherTimer.Stop();
            List_box.Items.Clear();
            num = 0;
           
            List_box.Items.Add(new Process.Task("Procec Name", "Threads Count", "Base Priority"," "));
            foreach (var proc in System.Diagnostics.Process.GetProcesses())
            {
                List_box.Items.Add(new Process.Task(proc.ProcessName, proc.Threads.Count.ToString(), proc.BasePriority.ToString(),proc.Id.ToString()));
                num += proc.Threads.Count;
                //if (proc.ProcessName == "notepad") proc.Kill();
            }
            Full_Count.Content = num.ToString();
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
           =>DragMove();
        
        private void List_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
            =>dispatcherTimer.Stop();
      
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            dispatcherTimer.Start();
        }
        private void Kill_Proces(string Name)
        {
            int NUMBER = Convert.ToInt32(Name);
          //  var pa = System.Diagnostics.Process.GetProcesses();
            foreach (var item in System.Diagnostics.Process.GetProcesses().OrderBy(x=>x.ProcessName))
            {
                if (item.Id == NUMBER)
                {
                    item.Kill();
                    return;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Tag)
                {
                    case "1":
                        if (List_box.Items.Count!=0&& List_box.SelectedItem is Process.Task a)
                            Kill_Proces(a.ID1);
                        dispatcherTimer.Start();
                        break;
                    default:
                        dispatcherTimer.Stop();
                        Close();
                        break;
                }
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            dispatcherTimer.Stop();
        }
    }
}
