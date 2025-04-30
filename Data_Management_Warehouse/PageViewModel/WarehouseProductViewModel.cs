using Data_Management_Warehouse.Command;
using Data_Management_Warehouse.ViewModel;
using Database.Context;
using Database.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApplicationApp.View.Pages
{
    public class WarehouseProductViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context = new SqlServerDbContext();
        private WarehouseProduct _editableWarehouseProduct;
        private bool _isEditMode;

        public ObservableCollection<WarehouseProduct> WarehouseProducts { get; set; }
        public WarehouseProduct SelectedWarehouseProduct { get; set; }

        public long ProductCount { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        // События для сообщений
        public event Action<string> ShowMessage;

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public WarehouseProductViewModel(WarehouseProduct warehouseProductToEdit = null)
        {
            LoadWarehouseProducts();

            SaveCommand = new RelayCommand(SaveWarehouseProduct);
            DeleteCommand = new RelayCommand(DeleteWarehouseProduct);

            if (warehouseProductToEdit != null)
            {
                _editableWarehouseProduct = warehouseProductToEdit;
                ProductCount = _editableWarehouseProduct.CountProduct;
                ProductId = _editableWarehouseProduct.ProductId;
                WarehouseId = _editableWarehouseProduct.WarehouseId;
                _isEditMode = true;
            }
        }

        private void LoadWarehouseProducts()
        {
            WarehouseProducts = new ObservableCollection<WarehouseProduct>(_context.WarehouseProducts.ToList());
            OnPropertyChanged(nameof(WarehouseProducts));
        }

        private void SaveWarehouseProduct(object obj)
        {
            if (ProductCount <= 0 || ProductId == 0 || WarehouseId == 0)
            {
                ShowMessage?.Invoke("Пожалуйста, заполните все поля корректно.");
                return;
            }

            if (_isEditMode)
            {
                // Редактирование
                var product = _context.WarehouseProducts.FirstOrDefault(p => p.Id == _editableWarehouseProduct.Id);
                if (product != null)
                {
                    product.CountProduct = ProductCount;
                    product.ProductId = ProductId;
                    product.WarehouseId = WarehouseId;

                    _context.SaveChanges();
                    ShowMessage?.Invoke("Данные успешно обновлены.");
                }
            }
            else
            {
                // Добавление
                var newWarehouseProduct = new WarehouseProduct
                {
                    CountProduct = ProductCount,
                    ProductId = ProductId,
                    WarehouseId = WarehouseId
                };

                _context.WarehouseProducts.Add(newWarehouseProduct);
                _context.SaveChanges();
                ShowMessage?.Invoke("Товар на складе успешно добавлен.");
            }

            LoadWarehouseProducts(); // Обновить список после сохранения
            ClearFields();
        }

        private void DeleteWarehouseProduct(object obj)
        {
            if (SelectedWarehouseProduct == null)
            {
                ShowMessage?.Invoke("Выберите запись для удаления.");
                return;
            }

            _context.WarehouseProducts.Remove(SelectedWarehouseProduct);
            _context.SaveChanges();
            ShowMessage?.Invoke("Запись успешно удалена.");

            LoadWarehouseProducts(); // Обновить список после удаления
        }

        private void ClearFields()
        {
            ProductCount = 0;
            ProductId = 0;
            WarehouseId = 0;
            _editableWarehouseProduct = null;
            _isEditMode = false;

            OnPropertyChanged(nameof(ProductCount));
            OnPropertyChanged(nameof(ProductId));
            OnPropertyChanged(nameof(WarehouseId));
        }
    }
}
