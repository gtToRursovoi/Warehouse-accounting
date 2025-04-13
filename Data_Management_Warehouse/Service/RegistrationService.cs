using Database.Context;
using Database.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Management_Warehouse.Service
{
    public class RegistrationService
    {
        private readonly SqlServerDbContext _context;

        public RegistrationService(SqlServerDbContext context)
        {
            _context = context;
        }

     
        public async Task<string> RegisterUserAsync(string login, string password)
        {
            // Проверка на уникальность логина
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (existingUser != null)
            {
                return "Пользователь с таким логином уже существует.";
            }

            // Создаем нового пользователя
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Login = login,
                Password = password // Пароль в открытом виде
            };

            // Добавляем нового пользователя в базу данных
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return "Регистрация прошла успешно!";
        }
    }
}
