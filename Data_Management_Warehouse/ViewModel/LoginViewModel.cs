using Data_Management_Warehouse.Command;
using Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Data_Management_Warehouse.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public event Action<string> OnError;
        public event Action<string> OnSuccess;
        public event Action CloseLoginWindow;

        public LoginViewModel()
        {
            _context = new SqlServerDbContext();
            LoginCommand = new RelayCommand(LoginExecute);
            CloseCommand = new RelayCommand(CloseLoginView);
        }

        private void LoginExecute(object obj)
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                OnError?.Invoke("Введите логин и пароль.");
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user == null)
            {
                OnError?.Invoke("Неверный логин или пароль.");
                return;
            }

            OnSuccess?.Invoke("Вы успешно вошли в систему!");
            ClearFields();
        }

        private void CloseLoginView(object obj)
        {
            CloseLoginWindow?.Invoke();
        }

        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}
