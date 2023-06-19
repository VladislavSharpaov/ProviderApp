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
    /// Логика взаимодействия для ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        public ContractPage()
        {
            InitializeComponent();

            ContractFilter();
        }
        private void ContractFilter()
        {
            List<Contract> contractList = ProviderDatabase.GetContext().Contract.ToList();

            if(!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                contractList = contractList.Where(x=>x.Client.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            ContractListBox.ItemsSource = contractList;
        }
        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddContractPage(null));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContractFilter();
        }

        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Contract selectedContract = button.DataContext as Contract;
            NavigationService.Navigate(new AddContractPage(selectedContract));
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ContractFilter();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContractFilter();
        }
    }
}
