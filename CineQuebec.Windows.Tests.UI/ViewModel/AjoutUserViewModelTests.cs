using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.InterfacesRepositorie;
using CineQuebec.Windows.DAL.Services;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.Exceptions.AbonneExceptions.Username;
using CineQuebec.Windows.ViewModel;
using Moq;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class AjoutUserViewModelTests
    {
        private readonly Mock<IAbonneService> _mockAbonneService;
        private readonly AjoutUserViewModel _viewModel;

        public AjoutUserViewModelTests()
        {
            // Arrange
            _mockAbonneService = new Mock<IAbonneService>();
            _viewModel = new AjoutUserViewModel(_mockAbonneService.Object);
        }

        [Fact]
        public void CanSignUp_ShouldReturnFalse_WhenObservableUsersignInLogInIsInvalid_For_Username()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new System.Security.SecureString();
            _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar('p');
            _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar('a');
            _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar('s');
            _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar('s');

            // Act
            bool canSignUp = ((DelegateCommand)_viewModel.SaveCommand).CanExecute();

            // Assert
            Assert.False(canSignUp);
        }
        [Fact]
        public void CanSignUp_ShouldReturnFalse_WhenObservableUsersignInLogInIsInvalid_For_Password()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "user1";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new System.Security.SecureString();

            // Act
            bool canSignUp = ((DelegateCommand)_viewModel.SaveCommand).CanExecute();

            // Assert
            Assert.False(canSignUp);
        }

        [Fact]
        public void CanSignUp_ShouldReturnTrue_WhenObservableUsersignInLogInIsValid()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "validUsername";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new System.Security.SecureString();
            foreach (char c in "validPassword123")
            {
                _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar(c);
            }

            // Act
            bool canSignUp = ((DelegateCommand)_viewModel.SaveCommand).CanExecute();

            // Assert
            Assert.True(canSignUp);
        }        

    }
}
