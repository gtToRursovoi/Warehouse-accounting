using Data_Management_Warehouse.Command;
using Data_Management_Warehouse.Service;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Data_Management_Warehouse.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly RegistrationService _registrationService;

        // Свойства для привязки в XAML
        public string Login { get; set; }
        public string Password { get; set; }

        // Команды
        public ICommand RegisterCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        // События для уведомлений View
        public event Action<string> OnError;
        public event Action<string> OnSuccess;
        public event Action CloseRegistrationWindow;

        public RegisterViewModel()
        {
            _registrationService = new RegistrationService(new SqlServerDbContext());
            RegisterCommand = new RelayCommand(async (obj) => await RegisterExecute());
            CloseCommand = new RelayCommand(CloseRegistrationView);
        }

        // Метод регистрации
        private async Task RegisterExecute()
        {
            // Проверка на пустые поля
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                OnError?.Invoke("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка на наличие букв и цифр в логине
            if (!IsValidLogin(Login))
            {
                OnError?.Invoke("Логин должен содержать только буквы и цифры.");
                return;
            }

            // Проверка на наличие букв и цифр в пароле
            if (!IsValidPassword(Password))
            {
                OnError?.Invoke("Пароль должен содержать только буквы и цифры.");
                return;
            }

            // Регистрация пользователя
            var result = await _registrationService.RegisterUserAsync(Login, Password);

            if (result == "Регистрация прошла успешно!")
            {
                OnSuccess?.Invoke(result);
                ClearFields();
            }
            else
            {
                OnError?.Invoke(result);
            }
          
        }

        // Метод для закрытия окна регистрации
        private void CloseRegistrationView(object obj)
        {
            CloseRegistrationWindow?.Invoke(); // Делегируем закрытие окна в представление
        }

        // Метод для очистки полей
        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
        }

        // Метод для проверки логина на наличие только букв и цифр
        private bool IsValidLogin(string login)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$"); // Регулярное выражение для букв и цифр
            return regex.IsMatch(login);
        }

        // Метод для проверки пароля на наличие только букв и цифр
        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$"); // Регулярное выражение для букв и цифр
            return regex.IsMatch(password);
        }
    }
}
