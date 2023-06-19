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
    /// Логика взаимодействия для AddContractPage.xaml
    /// </summary>
    public partial class AddContractPage : Page
    {
        private Contract _currentContract;
        public AddContractPage(Contract currentContract)
        {
            InitializeComponent();

            List<Client> clientList = ProviderDatabase.GetContext().Client.ToList();
            clientList.Insert(0, new Client
            {
                Name = "Клиент"
            });
            ClientComboBox.ItemsSource = clientList;
            ClientComboBox.DisplayMemberPath = "Name";

            List<Tariff> tariffList = ProviderDatabase.GetContext().Tariff.ToList();
            tariffList.Insert(0, new Tariff
            {
                Name = "Все тарифы"
            });
            TariffComboBox.ItemsSource = tariffList;
            TariffComboBox.DisplayMemberPath = "Name";


            if (currentContract == null)
            {
                _currentContract = new Contract();
                InfoTextBlock.Text = "Добавление нового контракта";
                AddContractButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Contract.Count() + 1).ToString();
                _currentContract.Date = DateTime.Now;
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentContract = currentContract;
                InfoTextBlock.Text = "Редактирование контракта";
                AddContractButton.Content = "Изменить";
                IdTextBox.Text = currentContract.ID.ToString();
                
            }
            DataContext = _currentContract;
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(AdressTextBox.Text))
            {
                stringBuilder.AppendLine("Введите адресс");
            }
            if (ClientComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите клиента");
            }
            if (TariffComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите тариф");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            
            ProviderDatabase.GetContext().Contract.AddOrUpdate(_currentContract);
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
