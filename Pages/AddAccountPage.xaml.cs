using ProviderApp.Databases;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddAccountPage.xaml
    /// </summary>
    public partial class AddAccountPage : Page
    {
        private Account _currentAccount;
        public AddAccountPage(Account currentAccount)
        {
            InitializeComponent();
             List<Worker> workerList = ProviderDatabase.GetContext().Worker.ToList();
            workerList.Insert(0, new Worker
            {
                Name = "Все сотрудники"
            });
            WorkerComboBox.ItemsSource = workerList;
            List<Access> accessList = ProviderDatabase.GetContext().Access.ToList();
            accessList.Insert(0, new Access
            {
                Name = "Все доступы"
            });
            AccessComboBox.ItemsSource = accessList;
            AccessComboBox.DisplayMemberPath = "Name";


            if (currentAccount == null)
            {
                _currentAccount = new Account();
                InfoTextBlock.Text = "Добавление аккаунта";
                AddAccountButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Account.Count() + 1).ToString();
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentAccount = currentAccount;
                InfoTextBlock.Text = "Редактирование аккаунта";
                AddAccountButton.Content = "Изменить";
                IdTextBox.Text = currentAccount.ID.ToString();
            }
            DataContext = _currentAccount;
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                stringBuilder.AppendLine("Введите логин");
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                stringBuilder.AppendLine("Введите пароль");
            }
            if (WorkerComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите сотрудника");
            }
            if (AccessComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите уровень доступа");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Account.AddOrUpdate(_currentAccount);
            try
            {

                ProviderDatabase.GetContext().SaveChanges();
                MessageBox.Show("Успешно сохранено");
                NavigationService.GoBack();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }
        }
    }
}
