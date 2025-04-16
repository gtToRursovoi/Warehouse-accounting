using ApplicationApp.View.Page;
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

namespace ApplicationApp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginViewWindow.xaml
    /// </summary>
    public partial class LoginViewWindow : Window
    {
        public LoginViewWindow()
        {
            InitializeComponent();
            var viewModel = new LoginViewModel();
            viewModel.OnError += ViewModel_OnError;
            viewModel.OnSuccess += ViewModel_OnSuccess;
            this.DataContext = viewModel;
        }
        private void ViewModel_OnError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ViewModel_OnSuccess(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Здесь можешь открыть главное окно приложения, например:
             new MainWindow().Show();
            this.Close();
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           App.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationViewWindow registrationViewWindow = new RegistrationViewWindow();
            registrationViewWindow.Show();
            this.Close();
        }
    }
}
