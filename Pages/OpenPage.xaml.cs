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
    /// Логика взаимодействия для OpenPage.xaml
    /// </summary>
    public partial class OpenPage : Page
    {
        private DataService _dataService;
        public OpenPage(DataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            DataContext = _dataService;
            
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Account selectedAccount = ProviderDatabase.GetContext().Account.FirstOrDefault(account=>account.Login.Equals(LoginTextBox.Text)&&account.Password.Equals(PasswordBox.Password));
            if (selectedAccount != null )
            {
                //DataService.SelectedAccount = selectedAccount;
                _dataService.SelectedAccount = selectedAccount;
                NavigationService.Navigate(new MenuPage(_dataService));
            }
            else
            {
                MessageBox.Show("Введен неверный логин или пароль", "Ошибка");
            }
            
        }
    }
}
