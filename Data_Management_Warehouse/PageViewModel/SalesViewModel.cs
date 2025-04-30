using Data_Management_Warehouse.Command;
using Data_Management_Warehouse.ViewModel;
using Database.Context;
using Database.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Data_Management_Warehouse.PageViewModel
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;
        private bool _isEditMode;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string> ShowMessage;

        // Коллекции
        public ObservableCollection<Sales> Sales { get; set; }
        public ObservableCollection<Agent> Agents { get; set; }
        public ObservableCollection<WarehouseProduct> WarehouseProducts { get; set; }

        // Свойства для привязки
        private Agent _selectedAgent;
        public Agent SelectedAgent
        {
            get => _selectedAgent;
            set { _selectedAgent = value; OnPropertyChanged(); }
        }

        private WarehouseProduct _selectedWarehouseProduct;
        public WarehouseProduct SelectedWarehouseProduct
        {
            get => _selectedWarehouseProduct;
            set { _selectedWarehouseProduct = value; OnPropertyChanged(); }
        }

        private int _salesCount;
        public int SalesCount
        {
            get => _salesCount;
            set { _salesCount = value; OnPropertyChanged(); }
        }

        private DateTime? _salesDate;
        public DateTime? SalesDate
        {
            get => _salesDate;
            set { _salesDate = value; OnPropertyChanged(); }
        }

        // ✅ Выбранная продажа (для редактирования или удаления)
        public Sales SelectedSale { get; set; }

        // Команды
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public SalesViewModel(Sales selectedSale = null)
        {
            _context = new SqlServerDbContext();

            Agents = new ObservableCollection<Agent>(_context.Agents.ToList());

            WarehouseProducts = new ObservableCollection<WarehouseProduct>(
    _context.WarehouseProducts
        .Include(wp => wp.Product) // Чтобы были доступны названия
        .ToList());

            Sales = new ObservableCollection<Sales>(
                _context.Sales
                .Include(s => s.Agent)
                .Include(s => s.WarehouseProduct)
                .ThenInclude(wp => wp.Product)
                .ToList());

            SaveCommand = new RelayCommand(_ => SaveExecute());
            DeleteCommand = new RelayCommand(_ => DeleteExecute());

            if (selectedSale != null)
            {
                SelectedSale = selectedSale;
                SelectedAgent = selectedSale.Agent;
                SelectedWarehouseProduct = selectedSale.WarehouseProduct;
                SalesCount = selectedSale.SalesCount;
                SalesDate = DateTime.TryParse(selectedSale.SalesDate, out DateTime date)
                    ? date
                    : (DateTime?)null;
                _isEditMode = true;
            }
        }

        private void SaveExecute()
        {
            if (SelectedAgent == null || SelectedWarehouseProduct == null || SalesDate == null || SalesCount <= 0)
            {
                ShowMessage?.Invoke("Пожалуйста, заполните все поля корректно.");
                return;
            }

            if (_isEditMode)
            {
                var sale = _context.Sales.FirstOrDefault(s => s.Id == SelectedSale.Id);
                if (sale != null)
                {
                    sale.AgentId = SelectedAgent.Id;
                    sale.WarehouseProductId = SelectedWarehouseProduct.Id;
                    sale.SalesCount = SalesCount;
                    sale.SalesDate = SalesDate.Value.ToShortDateString();

                    _context.SaveChanges();
                    ShowMessage?.Invoke("Продажа успешно обновлена.");
                }
            }
            else
            {
                var newSale = new Sales
                {
                    AgentId = SelectedAgent.Id,
                    WarehouseProductId = SelectedWarehouseProduct.Id,
                    SalesCount = SalesCount,
                    SalesDate = SalesDate.Value.ToShortDateString()
                };

                _context.Sales.Add(newSale);
                _context.SaveChanges();
                Sales.Add(newSale);
                ShowMessage?.Invoke("Продажа успешно добавлена.");
            }
        }

        private void DeleteExecute()
        {
            if (SelectedSale == null)
            {
                ShowMessage?.Invoke("Продажа не выбрана для удаления.");
                return;
            }

            var sale = _context.Sales.FirstOrDefault(s => s.Id == SelectedSale.Id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
                Sales.Remove(sale);
                ShowMessage?.Invoke("Продажа удалена.");
            }
        }
    }
}
