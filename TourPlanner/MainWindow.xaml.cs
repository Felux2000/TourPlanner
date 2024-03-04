﻿using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Forms;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new ViewModel();
            viewModel.Tours();
            DataContext = viewModel;
        }

    }

    public class Log
    {
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float Distance { get; set; }

        public Log(DateTime date, float duration, float distance) 
        {
            Date = date;
            Duration = duration;
            Distance = distance;
        }
    }

    public class Tour
    {
        public string Name { get; set; }
        public ObservableCollection<Log> LogList { get; set; }

        public Tour(string name, ObservableCollection<Log> logList)
        {
            Name = name;
            LogList = logList;
        }
    }

    
}