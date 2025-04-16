using Data_Management_Warehouse.ViewModel;
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
using System.Windows.Shapes;

namespace ApplicationApp.View.Page
{
    /// <summary>
    /// Логика взаимодействия для RegistrationViewWindow.xaml
    /// </summary>
    public partial class RegistrationViewWindow : Window
    {
        public RegistrationViewWindow()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();  
            InitializeComponent();
            this.DataContext = registerViewModel;

            registerViewModel.OnError += ViewModel_OnError;
            registerViewModel.OnSuccess += ViewModel_OnSuccess;
            
        }
        private void ViewModel_OnError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Обработчик события успеха
        private void ViewModel_OnSuccess(string successMessage)
        {
            MessageBox.Show(successMessage, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Обработчик закрытия окна
        private void ViewModel_CloseRegistrationWindow()
        {
            this.Close(); // Закрываем окно
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel_CloseRegistrationWindow();
        }
    }
}
