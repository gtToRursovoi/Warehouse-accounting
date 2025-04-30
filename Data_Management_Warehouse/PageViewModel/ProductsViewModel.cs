using Data_Management_Warehouse.Command;
using Data_Management_Warehouse.Interfaces;
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
    public class ProductsViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;
        private readonly IFileDialogService _fileDialogService;
        private Product _editableProduct;
        private bool _isEditMode;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Material> Materials { get; set; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private long _articul;
        public long Articul
        {
            get => _articul;
            set { _articul = value; OnPropertyChanged(nameof(Articul)); }
        }

        private decimal _minPrice;
        public decimal MinPrice
        {
            get => _minPrice;
            set { _minPrice = value; OnPropertyChanged(nameof(MinPrice)); }
        }

        private int _peopleMake;
        public int PeopleMake
        {
            get => _peopleMake;
            set { _peopleMake = value; OnPropertyChanged(nameof(PeopleMake)); }
        }

        private string _manafacturAdrres;
        public string ManafacturAdrres
        {
            get => _manafacturAdrres;
            set { _manafacturAdrres = value; OnPropertyChanged(nameof(ManafacturAdrres)); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set { _imagePath = value; OnPropertyChanged(nameof(ImagePath)); }
        }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged(nameof(SelectedMaterial));
            }
        }
        private int _materialId;
        public int MaterialId
        {
            get => _materialId;
            set { _materialId = value; OnPropertyChanged(nameof(MaterialId));}
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SelectImageCommand { get; }

        public event Action<string> ShowMessage;

        public ProductsViewModel(IFileDialogService fileDialogService ,Product productToEdit = null)
        {
            _context = new SqlServerDbContext();
            Products = new ObservableCollection<Product>(_context.Products.ToList());
            Materials = new ObservableCollection<Material>(_context.Materials.ToList());
            _fileDialogService = fileDialogService;
            SaveCommand = new RelayCommand(_ => Save());
            DeleteCommand = new RelayCommand(_ => Delete());
            SelectImageCommand = new RelayCommand(_ => SelectImage());

            

            if (productToEdit != null)
            {
                // Режим редактирования
                _editableProduct = productToEdit;
                Name = _editableProduct.Name;
                Articul = _editableProduct.Articul;
                MinPrice = _editableProduct.MinPrice;
                PeopleMake = _editableProduct.PeopleMake;
                ManafacturAdrres = _editableProduct.ManafacturAdrres;
                ImagePath = _editableProduct.ImagePath;
                SelectedMaterial = Materials.FirstOrDefault(m => m.Id == _editableProduct.MaterialId);
                MaterialId = _editableProduct.MaterialId;

                _isEditMode = true;
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Name)  || Articul <= 0 || MinPrice <= 0 || PeopleMake <= 0 || string.IsNullOrWhiteSpace(ManafacturAdrres))
            {
                ShowMessage?.Invoke("Пожалуйста, заполните все поля корректно.");
                return;
            }

            
                var newProduct = new Product
                {
                    Name = Name,
                    Articul = Articul,
                    MinPrice = MinPrice,
                    PeopleMake = PeopleMake,
                    ManafacturAdrres = ManafacturAdrres,
                    ImagePath = ImagePath,
                    MaterialId = MaterialId,
                };
                _context.Products.Add(newProduct);
                Products.Add(newProduct);
            

            _context.SaveChanges();
            ShowMessage?.Invoke(_isEditMode ? "Продукт успешно обновлен!" : "Продукт успешно добавлен!");

            ClearFields();
        }

        private void Delete()
        {
            if (SelectedProduct == null)
            {
                ShowMessage?.Invoke("Выберите продукт для удаления.");
                return;
            }

            _context.Products.Remove(SelectedProduct);
            _context.SaveChanges();
            Products.Remove(SelectedProduct);
            SelectedProduct = null;

            ShowMessage?.Invoke("Продукт успешно удален!");
        }

        private void SelectImage()
        {
            ImagePath = _fileDialogService.OpenFileDialog();
            OnPropertyChanged(nameof(ImagePath));
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Articul = 0;
            MinPrice = 0;
            PeopleMake = 0;
            ManafacturAdrres = string.Empty;
            ImagePath = null;
            SelectedMaterial = null;
            MaterialId = 0;
        }
    }
}
