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
    /// Логика взаимодействия для AddRequestPage.xaml
    /// </summary>
    public partial class AddRequestPage : Page
    {
        private Request _currentRequest;
        public AddRequestPage(Request currentRequest)
        {
            InitializeComponent();

            List<Worker> workerList = ProviderDatabase.GetContext().Worker.ToList();
            workerList.Insert(0, new Worker
            {
                Name = "Все сотрудники"
            });
            WorkerComboBox.ItemsSource = workerList;
            

            List<Service> serviceList = ProviderDatabase.GetContext().Service.ToList();
            serviceList.Insert(0, new Service
            {
                Name = "Все услуги"
            });
            ServiceComboBox.ItemsSource = serviceList;
            ServiceComboBox.DisplayMemberPath = "Name";

            List<Contract> contractList = ProviderDatabase.GetContext().Contract.ToList();
           
            ContractComboBox.ItemsSource = contractList;
            ContractComboBox.DisplayMemberPath = "ID";
            _currentRequest = currentRequest;

            if (currentRequest == null)
            {
                _currentRequest = new Request();
                InfoTextBlock.Text = "Добавление новой заявки";
                AddRequestButton.Content = "Добавить";
                IdTextBox.Text = (ProviderDatabase.GetContext().Request.Count() + 1).ToString();
                _currentRequest.Date = DateTime.Now;
                _currentRequest.Status = true;
                //CategoryComboBox.SelectedIndex = 0;
            }
            else
            {
                _currentRequest = currentRequest;
                InfoTextBlock.Text = "Редактирование контракта";
                AddRequestButton.Content = "Изменить";
                IdTextBox.Text = currentRequest.ID.ToString();

            }
            DataContext = _currentRequest;
        }

        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            StringBuilder stringBuilder = new StringBuilder();

            if (WorkerComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите сотрудника");
            }
            if (ContractComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите номер договора");
            }
            if (ServiceComboBox.SelectedIndex < 1)
            {
                stringBuilder.AppendLine("Выберите услугу");
            }

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            #endregion
            ProviderDatabase.GetContext().Request.AddOrUpdate(_currentRequest);
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
