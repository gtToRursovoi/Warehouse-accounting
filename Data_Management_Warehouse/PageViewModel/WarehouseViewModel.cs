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

namespace Data_Management_Warehouse.PageViewModel
{
    public class WarehouseViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;

        public ObservableCollection<Warehouse> Warehouses { get; set; }

        private Warehouse _selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => _selectedWarehouse;
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged();
            }
        }

        private Warehouse _editableWarehouse;
        private bool _isEditMode;

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _adress;
        public string Adress
        {
            get => _adress;
            set { _adress = value; OnPropertyChanged(); }
        }

        public event Action<string> ShowMessage;

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public WarehouseViewModel(Warehouse warehouseToEdit = null)
        {
            _context = new SqlServerDbContext();
            Warehouses = new ObservableCollection<Warehouse>(_context.Warehouses.ToList());

            SaveCommand = new RelayCommand(SaveWarehouse);
            DeleteCommand = new RelayCommand(DeleteWarehouse);

            if (warehouseToEdit != null)
            {
                _editableWarehouse = warehouseToEdit;
                Name = _editableWarehouse.Name;
                Adress = _editableWarehouse.Adress;
                _isEditMode = true;
            }
        }

        private void SaveWarehouse(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Adress))
            {
                ShowMessage?.Invoke("Пожалуйста, заполните все поля.");
                return;
            }

            if (_isEditMode && _editableWarehouse != null)
            {
                var warehouseInDb = _context.Warehouses.FirstOrDefault(w => w.Id == _editableWarehouse.Id);
                if (warehouseInDb == null)
                {
                    ShowMessage?.Invoke("Склад не найден.");
                    return;
                }

                warehouseInDb.Name = Name;
                warehouseInDb.Adress = Adress;
                _context.SaveChanges();

                var warehouseInList = Warehouses.FirstOrDefault(w => w.Id == _editableWarehouse.Id);
                if (warehouseInList != null)
                {
                    warehouseInList.Name = warehouseInDb.Name;
                    warehouseInList.Adress = warehouseInDb.Adress;
                    OnPropertyChanged(nameof(Warehouses));
                }
               

                ShowMessage?.Invoke("Склад успешно обновлён.");
            }
            else
            {
                var newWarehouse = new Warehouse
                {
                    Name = Name,
                    Adress = Adress
                };

                _context.Warehouses.Add(newWarehouse);
                _context.SaveChanges();

                Warehouses.Add(newWarehouse);

                ShowMessage?.Invoke("Склад успешно добавлен.");
            }

            ClearFields();
        }

        private void DeleteWarehouse(object obj)
        {
            if (SelectedWarehouse == null)
            {
                ShowMessage?.Invoke("Склад не выбран для удаления.");
                return;
            }

            var warehouseInDb = _context.Warehouses.FirstOrDefault(w => w.Id == SelectedWarehouse.Id);
            if (warehouseInDb == null)
            {
                ShowMessage?.Invoke("Склад не найден.");
                return;
            }

            _context.Warehouses.Remove(warehouseInDb);
            _context.SaveChanges();

            Warehouses.Remove(SelectedWarehouse);

            ShowMessage?.Invoke("Склад удалён.");
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Adress = string.Empty;
            _editableWarehouse = null;
            _isEditMode = false;
        }
    }
}
