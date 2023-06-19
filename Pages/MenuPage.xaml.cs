using ProviderApp.Classes;
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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private DataService _dataService;
        public MenuPage(DataService dataService)
        {
            _dataService = dataService;

            InitializeComponent();

            if (dataService.SelectedAccount.AccessID == 1)
            {
                DeviceButton.IsEnabled = false;
                TariffButton.IsEnabled = false;
                ClientButton.IsEnabled = false;
                RequestButton.IsEnabled = false;
                ContractButton.IsEnabled = false;
                //DeviceButton.ToolTip = "Недоступен для текущего уровня доступа";
            }
            if (dataService.SelectedAccount.AccessID == 3)
            {
                UserButton.IsEnabled = false;
                AccountButton.IsEnabled = false;
            }
        }
        private void DeviceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DevicePage());
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPage());
        }

        private void TariffButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TariffPage());
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestPage());
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void ContractButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractPage());
        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManualPage());
        }
    }
}
