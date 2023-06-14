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
    /// Логика взаимодействия для AddTariffPage.xaml
    /// </summary>
    public partial class AddTariffPage : Page
    {
        private Tariff _currentTariff;
        public AddTariffPage(Tariff currentTariff)
        {
            InitializeComponent();

            if (currentTariff == null)
            {
                _currentTariff = new Tariff();
                InfoTextBlock.Text = "Добавление нового тарифа";
                AddTariffButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Tariff.Count() + 1).ToString();
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentTariff = currentTariff;
                InfoTextBlock.Text = "Редактирование тарифа";
                AddTariffButton.Content = "Изменить";
                IdTextBox.Text = currentTariff.ID.ToString();
            }
            DataContext = _currentTariff;

        }

        private void AddTariffButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                stringBuilder.AppendLine("Введите наименование");
            }
            if (string.IsNullOrEmpty(CostTextBox.Text))
            {
                stringBuilder.AppendLine("Введите цену");
            }
            if (string.IsNullOrEmpty(SpeedTextBox.Text))
            {
                stringBuilder.AppendLine("Введите скорость тарифа");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Tariff.AddOrUpdate(_currentTariff);
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
