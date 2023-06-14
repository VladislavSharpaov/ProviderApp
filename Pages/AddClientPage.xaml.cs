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
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        private Client _currentClient;
        public AddClientPage(Client currentClient)
        {
            InitializeComponent();

            List<ProviderApp.Databases.Type> categoryList = ProviderDatabase.GetContext().Type.ToList();
            categoryList.Insert(0, new ProviderApp.Databases.Type
            {
                Name = "Все категории"
            });
            TypeComboBox.ItemsSource = categoryList;
            TypeComboBox.DisplayMemberPath = "Name";


            if (currentClient == null)
            {
                _currentClient = new Client();
                InfoTextBlock.Text = "Добавление нового клиента";
                AddClientButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Client.Count() + 1).ToString();
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentClient = currentClient;
                InfoTextBlock.Text = "Редактирование клиента";
                AddClientButton.Content = "Изменить";
                IdTextBox.Text = currentClient.ID.ToString();
            }
            DataContext = _currentClient;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                stringBuilder.AppendLine("Введите наименование");
            }
            if (TypeComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите тип клиента");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Client.AddOrUpdate(_currentClient);
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
