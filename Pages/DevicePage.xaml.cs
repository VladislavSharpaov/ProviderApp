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
    /// Логика взаимодействия для DevicePage.xaml
    /// </summary>
    public partial class DevicePage : Page
    {
        public DevicePage()
        {
            InitializeComponent();

            List<Category> categoryList = ProviderDatabase.GetContext().Category.ToList();
            categoryList.Insert(0, new Category
            {
                Name = "Все категории"
            });
            CategoryComboBox.ItemsSource = categoryList;
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedIndex = 0;
            

            FilterDevice();
        }
        private void FilterDevice()
        {
            List<Device> deviceList = ProviderDatabase.GetContext().Device.ToList();

            if (CategoryComboBox.SelectedIndex > 0 )
            {
                deviceList = deviceList.Where(x => x.Category.Equals(CategoryComboBox.SelectedItem)).ToList();
            }
            if(!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                deviceList = deviceList.Where(x =>x.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            DeviceListBox.ItemsSource = deviceList;
        }
        private void DeviceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDevicePage(null));
        }
        
        private void ChangeDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Device selectedDevice = button.DataContext as Device;
            NavigationService.Navigate(new AddDevicePage(selectedDevice));
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDevice();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterDevice();
        }

        private void Page_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FilterDevice();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FilterDevice();
        }
    }
}
