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
        public MenuPage()
        {
            InitializeComponent();
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

        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContractButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
