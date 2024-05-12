using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineQuebec.Windows.ViewModel
{
    public class CategoriePrefereeUserControlModel : PropertyNotifier
    {
        private IAbonneService _abonneService;
        private Abonne _abonne { get; set; } = new();
        public event Action<string> ErrorOccurred;
        private Categories _selectedCategorie = new();
        private Categories _deleteSelectedCategorie = new();
        private ObservableCollection<Categories> _categoriesPreferee = [];
        public ICommand AddCategorieCommand { get; init; }
        public ICommand DeleteCategorieCommand { get; init; }
        public Categories SelectedCategorie
        {
            get => _selectedCategorie;
            set
            {
                _selectedCategorie = value;
                OnPropertyChanged(nameof(SelectedCategorie));
            }
        }
        public Categories DeleteSelectedCategorie
        {
            get => _deleteSelectedCategorie;
            set
            {
                _deleteSelectedCategorie = value;
                OnPropertyChanged(nameof(DeleteSelectedCategorie));
            }
        }
        public ObservableCollection<Categories> Categories { get; init; } = [];
        public ObservableCollection<Categories> CategoriesPreferee
        {
            get => _categoriesPreferee;

            set
            {
                _categoriesPreferee = value;
                OnPropertyChanged(nameof(CategoriesPreferee));
            }
        }

        public CategoriePrefereeUserControlModel(IAbonneService abonneService, Abonne abonne)
        {
            this._abonneService = abonneService;
            this._abonne = abonne;
            AddCategorieCommand = new DelegateCommand(AddCategorie);
            DeleteCategorieCommand = new DelegateCommand(DeleteCategorie);
        }
        internal void Loaded(object sender, RoutedEventArgs e)
        {
            ChargerCategorie();
            ChargerCategoriesPreferee();
        }
        public void ChargerCategoriesPreferee()
        {
            CategoriesPreferee.Clear();
            foreach (var categorie in _abonne.CategoriesPrefere)
            {
                CategoriesPreferee.Add(categorie);
            }
        }
        private void ChargerCategorie()
        {
            Categories.Clear();
            foreach (var cat in Enum.GetValues(typeof(Categories)).Cast<Categories>())
            {
                Categories.Add(cat);
            }
        }
        public async void DeleteCategorie()
        {
            try
            {
                await _abonneService.RemoveCategorieInAbonne(_abonne, DeleteSelectedCategorie);
                CategoriesPreferee.Remove(DeleteSelectedCategorie);
                DeleteSelectedCategorie = new();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }

        public async void AddCategorie()
        {
            try
            {
                await _abonneService.AddCategorieInAbonne(_abonne, SelectedCategorie);
                CategoriesPreferee.Add(SelectedCategorie);
                SelectedCategorie = new();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(ex.Message);
            }
        }
    }
}