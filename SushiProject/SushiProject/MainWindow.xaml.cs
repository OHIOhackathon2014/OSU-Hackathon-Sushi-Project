﻿using SushiProject.ViewModels;
using SushiProject.Views;
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

namespace SushiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SettingsView settingsView = new SettingsView();
        }

        private void ProjectSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView();
            settingsView.Owner = Window.GetWindow(this);
            MainWindowViewModel mwvm = ((MainWindowViewModel)DataContext);
            SettingsViewModel svm = mwvm.Settings;
            svm.Project = mwvm.Project;
            settingsView.DataContext = svm;
            settingsView.Show();
        }
    }
}
