
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Enums;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using Moq;
using Prism.Logging;

namespace CineQuebec.Windows.Tests.UI
{
    public class ListPreferecesViewModelTests
    {
        Mock<IAbonneService> _abonneService;
        Mock<IRealisateurRepository> _realisateurService;
        Mock<IActeurRepository> _acteurService;
        Mock<IFilmService> _filmsService;
        Abonne abonne;
        List<Realisateur> realisateurs;
        List<Acteur> acteurs;
        List<Film> films;
        List<Categories> categories;
        public ListPreferecesViewModelTests()
        {
            _abonneService = new Mock<IAbonneService>();
            _realisateurService = new Mock<IRealisateurRepository>();
            _acteurService = new Mock<IActeurRepository>();
            _filmsService = new Mock<IFilmService>();
            realisateurs = new List<Realisateur>
            {
                new Realisateur(),
                new Realisateur(),
            };
            acteurs = new List<Acteur>
            {
                new Acteur(),
                new Acteur()
            };
            films = new List<Film>
            {
                new Film(),
                new Film()
            };
            categories = new List<Categories>
            {
                Categories.ACTION,
                Categories.COMEDY
            };
            abonne = new Abonne
            {
                Realisateurs = realisateurs,
                Acteurs = acteurs,
                Films = films,
                CategoriesPrefere = categories
            };


        }

        [Fact]
        public void ChargerRealisateursPreferee_Should_GetAll_Realisateurs_That_Are_In_Abonne()
        {
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _viewModel.ChargerRealisateursPreferee();

            Assert.Equal(realisateurs, abonne.Realisateurs);
        }

        [Fact]
        public void ChargerActeursPreferee_Should_GetAll_Acteurs_That_Are_In_Abonne()
        {
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _viewModel.ChargerActeursPreferee();

            Assert.Equal(acteurs, abonne.Acteurs);
        }

        [Fact]
        public void ChargerFilmsPreferee_Should_GetAll_Films_That_Are_In_Abonne()
        {
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _viewModel.ChargerFilmsPreferee();

            Assert.Equal(films, abonne.Films);
        }

        [Fact]
        public void ChargerCategoriePreferee_Should_GetAll_Categories_That_Are_In_Abonne()
        {
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _viewModel.ChargerCategoriesPreferee();

            Assert.Equal(categories, abonne.CategoriesPrefere);
        }


        [Fact]
        public void ClickOnAddRealisateurBouton_Should_AddRealisateur_To_RealisateruPreferees_In_Abonne()
        {
            var realisateur = new Realisateur();

            realisateurs.Add(realisateur);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.AddRealisateurInAbonne(abonne, realisateur)).ReturnsAsync(abonne);
            _viewModel.AddRealisateur();


            Assert.Equal(realisateurs, abonne.Realisateurs);
        }

        [Fact]
        public void ClickOnAddActeurBouton_Should_AddActeur_To_ActeursPreferees_In_Abonne()
        {
            var acteur = new Acteur();

            acteurs.Add(acteur);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.AddActeurInAbonne(abonne, acteur)).ReturnsAsync(abonne);
            _viewModel.AddActeur();


            Assert.Equal(acteurs, abonne.Acteurs);
        }
        [Fact]
        public void ClickOnAddFilmBouton_Should_AddFilm_To_FilmsPreferees_In_Abonne()
        {
            var film = new Film();

            films.Add(film);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.AddFilmInAbonne(abonne, film)).ReturnsAsync(abonne);
            _viewModel.AddFilm();


            Assert.Equal(films, abonne.Films);
        }

        [Fact]
        public void ClickOnAddCategorieBouton_Should_AddCategorie_To_CategoriesPreferees_In_Abonne()
        {
            var categorie = Categories.DOCUMENTARY;

            categories.Add(categorie);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.AddCategorieInAbonne(abonne, categorie)).ReturnsAsync(abonne);
            _viewModel.AddCategorie();


            Assert.Equal(categories, abonne.CategoriesPrefere);
        }
        [Fact]
        public void ClickOnDeleteRealisateurBouton_Should_DeleteRealisateur_From_RealisateruPreferees_In_Abonne()
        {
            var realisateur = new Realisateur();
            realisateurs.Add(realisateur);

            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.RemoveRealisateurInAbonne(abonne, realisateur)).ReturnsAsync(abonne);
            _viewModel.DeleteRealisateur();
            realisateurs.Remove(realisateur);

            Assert.Equal(realisateurs, abonne.Realisateurs);
        }

        [Fact]
        public void ClickOnDeleteActeurBouton_Should_DeleteActeur_From_ActeursPreferees_In_Abonne()
        {
            var acteur = new Acteur();
            acteurs.Add(acteur);

            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.RemoveActeurInAbonne(abonne, acteur)).ReturnsAsync(abonne);
            _viewModel.DeleteActeur();
            acteurs.Remove(acteur);

            Assert.Equal(acteurs, abonne.Acteurs);
        }

        [Fact]
        public void ClickOnDeleteFilmBouton_Should_DeleteFilm_From_FilmsPreferees_In_Abonne()
        {
            var film = new Film();
            films.Add(film);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.RemoveFilmInAbonne(abonne, film)).ReturnsAsync(abonne);
            _viewModel.AddFilm();
            films.Remove(film);

            Assert.Equal(films, abonne.Films);
        }

        [Fact]
        public void ClickOnDeleteCategorieBouton_Should_DeleteCategorie_To_CategoriesPreferees_In_Abonne()
        {
            var categorie = Categories.DOCUMENTARY;

            categories.Add(categorie);
            var _viewModel = new ListPreferencesViewModel(_abonneService.Object, _realisateurService.Object, _acteurService.Object, _filmsService.Object, abonne);


            _abonneService.Setup(x => x.RemoveCategorieInAbonne(abonne, categorie)).ReturnsAsync(abonne);
            _viewModel.AddCategorie();
            categories.Remove(categorie);

            Assert.Equal(categories, abonne.CategoriesPrefere);
        }
    }
}