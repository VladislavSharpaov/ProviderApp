using ProviderApp.Classes;
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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        
        public AccountPage()
        {
           

            InitializeComponent();

            AccountFilter();

            
            
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AccountFilter()
        {
            List<Account> accountList = ProviderDatabase.GetContext().Account.ToList();

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                accountList = accountList.Where(x =>x.Worker.Surname.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
                //     if (!string.IsNullOrEmpty(SearchTextBox.Text))
                //{
                //    deviceList = deviceList.Where(x => x.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
                //}
                
            }

            AccountListBox.ItemsSource = accountList;
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AccountFilter();
        }
    }
}
