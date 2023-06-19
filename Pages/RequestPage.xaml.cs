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
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();

            FilterRequest();
        }
        private void FilterRequest()
        {
            List<Request> requestList = ProviderDatabase.GetContext().Request.ToList();



            RequestListBox.ItemsSource = requestList;
        }
        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRequestPage(null));
        }

        private void ChangeRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Request selectedRequest = button.DataContext as Request;
            NavigationService.Navigate(new AddRequestPage(selectedRequest));
        }

 

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRequest();
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FilterRequest();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FilterRequest();
        }
    }
}
