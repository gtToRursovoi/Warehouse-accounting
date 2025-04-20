using Data_Management_Warehouse.Command;
using Data_Management_Warehouse.ViewModel;
using Database.Context;
using Database.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Data_Management_Warehouse.PageViewModel
{
    public class AgentViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;
        private  bool _isEditMode;

        public int AgentId { get; set; }  // Для редактирования

        public string Name { get; set; }
        public string INN { get; set; }
        public string LegalAddress { get; set; }
        public string Director { get; set; }
        private Agent _selectedAgent;
        public Agent SelectedAgent
        {
            get => _selectedAgent;
            set
            {
                _selectedAgent = value;
                OnPropertyChanged();

                if (_selectedAgent != null)
                {
                    // Переключаемся в режим редактирования
                    _isEditMode = true;
                    AgentId = _selectedAgent.Id;
                    Name = _selectedAgent.Name;
                    INN = _selectedAgent.INN.ToString();
                    LegalAddress = _selectedAgent.LegalAdress;
                    Director = _selectedAgent.Directro;
                }
            }
        }

        public ObservableCollection<Agent> Agents { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand LoadAgentsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public event Action<string> OnError;
        public event Action<string> OnSuccess;
        public event Action CloseWindow;

        public AgentViewModel(Agent existingAgent = null)
        {
            _context = new SqlServerDbContext();
            Agents = new ObservableCollection<Agent>();

            if (existingAgent != null)
            {
                _isEditMode = true;
                AgentId = existingAgent.Id;
                Name = existingAgent.Name;
                INN = existingAgent.INN.ToString();
                LegalAddress = existingAgent.LegalAdress;
                Director = existingAgent.Directro;
            }

            SaveCommand = new RelayCommand(SaveAgentExecute);
            CloseCommand = new RelayCommand(obj => CloseWindow?.Invoke());
            LoadAgentsCommand = new RelayCommand(LoadAgents); // Привязываем команду для загрузки агентов
            DeleteCommand = new RelayCommand(DeleteAgentExecute);

            // Автоматическая загрузка агентов при создании ViewModel
            LoadAgents(null);
        }

        private void SaveAgentExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(INN) ||
                string.IsNullOrWhiteSpace(LegalAddress) || string.IsNullOrWhiteSpace(Director))
            {
                OnError?.Invoke("Пожалуйста, заполните все поля.");
                return;
            }

            if (!Regex.IsMatch(INN, @"^\d{10}$"))
            {
                OnError?.Invoke("ИНН должен состоять из 10 цифр.");
                return;
            }

            if (_isEditMode)
            {
                var agent = _context.Agents.FirstOrDefault(a => a.Id == AgentId);
                if (agent == null)
                {
                    OnError?.Invoke("Агент не найден.");
                    return;
                }

                agent.Name = Name;
                agent.INN = long.Parse(INN);
                agent.LegalAdress = LegalAddress;
                agent.Directro = Director;

                _context.SaveChanges();
                OnSuccess?.Invoke("Агент успешно обновлён.");
            }
            else
            {
                if (_context.Agents.Any(a => a.INN.ToString() == INN))
                {
                    OnError?.Invoke("Агент с таким ИНН уже существует.");
                    return;
                }

                var newAgent = new Agent
                {
                    Name = Name,
                    INN = long.Parse(INN),
                    LegalAdress = LegalAddress,
                    Directro = Director
                };

                _context.Agents.Add(newAgent);
                _context.SaveChanges();
                OnSuccess?.Invoke("Агент успешно добавлен.");
                ClearFields();
            }
        }

        private void DeleteAgentExecute(object obj)
        {
            if (SelectedAgent == null)
            {
                OnError?.Invoke("Выберите агента для удаления.");
                return;
            }

            _context.Agents.Remove(SelectedAgent);
            _context.SaveChanges();
            Agents.Remove(SelectedAgent);
            SelectedAgent = null;

            OnSuccess?.Invoke("Агент успешно удалён.");
            ClearFields();
        }

        private bool CanDeleteAgent(object obj)
        {
            return SelectedAgent != null;
        }

        private void ClearFields()
        {
            Name = string.Empty;
            INN = string.Empty;
            LegalAddress = string.Empty;
            Director = string.Empty;
        }
        private void LoadAgents(object obj)
        {
            // Загружаем агентов из базы данных
            var agentsList = _context.Agents.ToList();
            // Очистить текущий список
            Agents.Clear();

            if (agentsList != null)
            {
                foreach (var agent in agentsList)
                {
                    Agents.Add(agent);  // Добавляем каждого агента в коллекцию
                }
            }
            else
            {
                OnError?.Invoke("Не удалось загрузить агентов.");
            }
        }
    }
}
