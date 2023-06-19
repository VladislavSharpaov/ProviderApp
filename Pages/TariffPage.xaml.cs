using ProviderApp.Databases;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Логика взаимодействия для TariffPage.xaml
    /// </summary>
    public partial class TariffPage : Page
    {
        public TariffPage()
        {
            InitializeComponent();

            FilterTariff();
            
        }
        private void FilterTariff()
        {
            List<Tariff> tariffList = ProviderDatabase.GetContext().Tariff.ToList();
            TariffListBox.ItemsSource = tariffList;
        }
        private void TariffButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTariffPage(null));
        }

        private void ChangeTariffButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Tariff selectedTariff = button.DataContext as Tariff;
            NavigationService.Navigate(new AddTariffPage(selectedTariff));
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FilterTariff();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FilterTariff();
        }
    }
}
