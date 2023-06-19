﻿using ProviderApp.Classes;
using ProviderApp.Pages;
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

namespace ProviderApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private DataService _dataService;
        public MenuWindow()
        {
            InitializeComponent();

            _dataService = new DataService();
            DataContext = _dataService;

            //if (DataService.SelectedAccount != null)
            //{
            //    NameTextBlock.Text = DataService.SelectedAccount.Worker.Name;
            //}
            MenuFrame.Navigate(new OpenPage(_dataService));
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void StateButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Maximized;

            }
            else
            {
                ResizeMode = ResizeMode.CanResize;
                WindowState = WindowState.Normal;
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                ResizeMode = ResizeMode.CanResize; 
            }
            else if (WindowState == WindowState.Minimized)
            {
                ResizeMode = ResizeMode.NoResize;
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new MenuPage(_dataService));
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new OpenPage(_dataService));
        }
    }
}
