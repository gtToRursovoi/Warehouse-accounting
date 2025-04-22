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
    public class MaterialsViewModel : BaseViewModel
    {
        private readonly SqlServerDbContext _context;
        private bool _isEditMode;
        private Material _editableMaterial;

        public ObservableCollection<Material> Materials { get; set; }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged();

                if (_selectedMaterial != null)
                {
                    Name = _selectedMaterial.Name;
                    CountMaterials = _selectedMaterial.CountMaterials;
                    TypeMaterial = _selectedMaterial.TypeMaterial;
                    _editableMaterial = _selectedMaterial;
                    _isEditMode = true;
                }
            }
        }

        public string Name { get; set; }
        public string CountMaterials { get; set; }
        public string TypeMaterial { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand DeleteMaterialCommand { get; }

        public event Action<string> ShowMessage;

        public MaterialsViewModel(Material materialToEdit = null)
        {
            _context = new SqlServerDbContext();
            Materials = new ObservableCollection<Material>(_context.Materials.ToList());

            SaveCommand = new RelayCommand(SaveMaterial);
            DeleteMaterialCommand = new RelayCommand(DeleteMaterial);

            if (materialToEdit != null)
            {
                _editableMaterial = materialToEdit;
                Name = _editableMaterial.Name;
                CountMaterials = _editableMaterial.CountMaterials;
                TypeMaterial = _editableMaterial.TypeMaterial;
                _isEditMode = true;
            }
        }

        private void SaveMaterial(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(CountMaterials) || string.IsNullOrWhiteSpace(TypeMaterial))
            {
                ShowMessage?.Invoke("Пожалуйста, заполните все поля.");
                return;
            }

            if (_isEditMode && _editableMaterial != null)
            {
                // Обновляем свойства существующего материала
                var materialFromDb = _context.Materials.FirstOrDefault(m => m.Id == _editableMaterial.Id);
                if (materialFromDb != null)
                {
                    materialFromDb.Name = Name;
                    materialFromDb.CountMaterials = CountMaterials;
                    materialFromDb.TypeMaterial = TypeMaterial;
                }
                else
                {
                    ShowMessage?.Invoke("Материал не найден в базе данных.");
                    return;
                }
            }
            else
            {
                // Добавление нового материала
                var newMaterial = new Material
                {
                    Name = Name,
                    CountMaterials = CountMaterials,
                    TypeMaterial = TypeMaterial
                };

                _context.Materials.Add(newMaterial);
            }

            _context.SaveChanges();
            ShowMessage?.Invoke(_isEditMode ? "Материал обновлен." : "Материал добавлен.");
        }
        private void DeleteMaterial(object obj)
        {
            if (_editableMaterial == null)
            {
                ShowMessage?.Invoke("Материал не выбран для удаления.");
                return;
            }

            var materialInDb = _context.Materials.FirstOrDefault(m => m.Id == _editableMaterial.Id);
            if (materialInDb == null)
            {
                ShowMessage?.Invoke("Материал не найден.");
                return;
            }

            // Запрос на удаление материала
            _context.Materials.Remove(materialInDb);
            _context.SaveChanges();

            // Удаляем материал из коллекции
            Materials.Remove(_editableMaterial);

            ShowMessage?.Invoke("Материал удалён.");

            // Очистить поля и выйти из режима редактирования
            Name = string.Empty;
            CountMaterials = string.Empty;
            TypeMaterial = string.Empty;
            _editableMaterial = null;
            _isEditMode = false;
        }

        private void ClearFields()
        {
            Name = string.Empty;
            CountMaterials = string.Empty;
            TypeMaterial = string.Empty;

            SelectedMaterial = null;
            _editableMaterial = null;
            _isEditMode = false;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(CountMaterials));
            OnPropertyChanged(nameof(TypeMaterial));
            OnPropertyChanged(nameof(SelectedMaterial));
        }
    }
}
