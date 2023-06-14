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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();

            List<ProviderApp.Databases.Type> typeList = ProviderDatabase.GetContext().Type.ToList();
            typeList.Insert(0, new ProviderApp.Databases.Type
            {
                Name = "Все категории"
            });
            TypeComboBox.ItemsSource = typeList;
            TypeComboBox.DisplayMemberPath = "Name";
            TypeComboBox.SelectedIndex = 0;

            ClientFilter();
        }

        private void ClientFilter()
        {
            List<Client> clientList = ProviderDatabase.GetContext().Client.ToList();

            if (TypeComboBox.SelectedIndex > 0)
            {
                clientList = clientList.Where(x => x.Type.Equals(TypeComboBox.SelectedItem)).ToList();
            }
            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                clientList = clientList.Where(x=>x.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            ClientListBox.ItemsSource = clientList;
        }
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddClientPage(null));
        }

        private void ChangeClientButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Client selectedClient = button.DataContext as Client;
            NavigationService.Navigate(new AddClientPage(selectedClient));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClientFilter();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientFilter();
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClientFilter();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientFilter();
        }
    }
}
