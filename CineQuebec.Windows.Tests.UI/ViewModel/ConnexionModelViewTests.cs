using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using CineQuebec.Windows.ViewModel;
using Moq;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.UI.ViewModel
{
    public class ConnexionModelViewTests
    {
        private readonly Mock<IAbonneService> _mockAbonneService;
        private readonly ConnexionModelView _viewModel;

        public ConnexionModelViewTests()
        {
            // Arrange
            _mockAbonneService = new Mock<IAbonneService>();
            _viewModel = new ConnexionModelView(_mockAbonneService.Object);
        }

        [Fact]
        public void CanLogIn_ShouldReturnFalse_WhenObservableUsersignInLogInIsInvalid()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new SecureString();

            // Act
            bool canLogIn = ((DelegateCommand)_viewModel.SaveCommand).CanExecute();

            // Assert
            Assert.False(canLogIn);
        }

        [Fact]
        public void CanLogIn_ShouldReturnTrue_WhenObservableUsersignInLogInIsValid()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "validUsername";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new SecureString();
            foreach (char c in "validPassword123")
            {
                _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar(c);
            }

            // Act
            bool canLogIn = ((DelegateCommand)_viewModel.SaveCommand).CanExecute();

            // Assert
            Assert.True(canLogIn);
        }

        [Fact]
        public async Task LogIn_ShouldInvokeGetAbonneConnexion_WhenValidDataProvided()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "validUsername";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new SecureString();
            foreach (char c in "validPassword123")
            {
                _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar(c);
            }

            var tcs = new TaskCompletionSource<bool>();
            _mockAbonneService.Setup(service => service.GetAbonneConnexion(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Abonne())
                .Callback(() => tcs.SetResult(true));

            // Act
            _viewModel.LogIn();
            await tcs.Task; // Await the completion of the GetAbonneConnexion method

            // Assert
            _mockAbonneService.Verify(service => service.GetAbonneConnexion(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task LogIn_ShouldRaiseConnexionErreurEvent_WhenUserIsNotNull()
        {
            // Arrange
            _viewModel.ObservableUsersignInLogIn.Username = "validUsername";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new SecureString();
            foreach (char c in "validPassword123")
            {
                _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar(c);
            }

            _mockAbonneService.Setup(service => service.GetAbonneConnexion(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Abonne());

            bool connexionErreurRaised = false;
            bool result = false;

            _viewModel.ConnexionErreur += (res) => {
                connexionErreurRaised = true;
                result = res;
            };

            // Act
            _viewModel.LogIn();
            await Task.Delay(100); // Small delay to ensure async method completes

            // Assert
            Assert.True(connexionErreurRaised);
            Assert.True(result);
        }

        [Fact]
        public async Task LogIn_ShouldRaiseErrorOccuredEvent_WhenExceptionIsThrown()
        {
            // Arrange
            var errorMessage = "An error occurred";
            _viewModel.ObservableUsersignInLogIn.Username = "validUsername";
            _viewModel.ObservableUsersignInLogIn.SecurePassword = new SecureString();
            foreach (char c in "validPassword123")
            {
                _viewModel.ObservableUsersignInLogIn.SecurePassword.AppendChar(c);
            }

            _mockAbonneService.Setup(service => service.GetAbonneConnexion(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception(errorMessage));

            bool errorOccuredRaised = false;
            string receivedErrorMessage = null;

            _viewModel.ErrorOccured += (msg) => {
                errorOccuredRaised = true;
                receivedErrorMessage = msg;
            };

            // Act
            _viewModel.LogIn();
            await Task.Delay(100); // Small delay to ensure async method completes

            // Assert
            Assert.True(errorOccuredRaised);
            Assert.Equal(errorMessage, receivedErrorMessage);
        }

    }
}
