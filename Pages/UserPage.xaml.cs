using ProviderApp.Databases;
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

namespace ProviderApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        
        public UserPage()
        {
            InitializeComponent();

            WorkerFilter();
            
        }
        private void WorkerFilter()
        {
            List<Worker> workerList = ProviderDatabase.GetContext().Worker.ToList();

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                workerList = workerList.Where(x =>x.Surname.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            WorkerListBox.ItemsSource = workerList;
        }
        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(null));
        }

        private void ChangeWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Worker selectedWorker = button.DataContext as Worker;
            NavigationService.Navigate(new AddUserPage(selectedWorker));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WorkerFilter();
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            WorkerFilter();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WorkerFilter();
        }
    }
}
