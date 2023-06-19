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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private Worker _currentWorker;
        public AddUserPage(Worker currentWorker)
        {
            InitializeComponent();
            List < Role> roleList = ProviderDatabase.GetContext().Role.ToList();
            roleList.Insert(0, new Role
            {
                Name = "Все категории"
            });
            RoleComboBox.ItemsSource = roleList;
            RoleComboBox.DisplayMemberPath = "Name";


            if (currentWorker == null)
            {
                _currentWorker = new Worker();
                InfoTextBlock.Text = "Добавление нового сотрудника";
                AddWorkerButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Worker.Count() + 1).ToString();
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentWorker = currentWorker;
                InfoTextBlock.Text = "Редактирование сотрудника";
                AddWorkerButton.Content = "Изменить";
                IdTextBox.Text = currentWorker.ID.ToString();
            }
            DataContext = _currentWorker;
        }

       

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(SurnameTextBox.Text))
            {
                stringBuilder.AppendLine("Введите фамилию");
            }
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                stringBuilder.AppendLine("Введите имя");
            }
            if (string.IsNullOrEmpty(PatronymicTextBox.Text))
            {
                stringBuilder.AppendLine("Введите отчество");
            }
            if (RoleComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите роль");
            }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Worker.AddOrUpdate(_currentWorker);
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
