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
    /// Логика взаимодействия для AddDevicePage.xaml
    /// </summary>
    public partial class AddDevicePage : Page
    {
        private Device _currentDevice;
        public AddDevicePage(Device currentDevice)
        {
            InitializeComponent();

            List<Category> categoryList = ProviderDatabase.GetContext().Category.ToList();
            categoryList.Insert(0, new Category
            {
                Name = "Все категории"
            });
            CategoryComboBox.ItemsSource = categoryList;
            CategoryComboBox.DisplayMemberPath = "Name";


            if (currentDevice == null)
            {
                _currentDevice = new Device();
                InfoTextBlock.Text = "Добавление нового оборудования";
                AddDeviceButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Device.Count() + 1).ToString();
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentDevice = currentDevice;
                InfoTextBlock.Text = "Редактирование оборудования";
                AddDeviceButton.Content = "Изменить";
                IdTextBox.Text = currentDevice.ID.ToString();
            }
            DataContext = _currentDevice;



        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                stringBuilder.AppendLine("Введите наименование");
            }
            if (CategoryComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите категорию");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Device.AddOrUpdate(_currentDevice);
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
