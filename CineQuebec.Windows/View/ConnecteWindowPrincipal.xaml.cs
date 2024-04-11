using System.Windows;
using Unity;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConnecteWindowPrincipal.xaml
    /// </summary>
    public partial class ConnecteWindowPrincipal : Window
    {
        public ConnecteWindowPrincipal()
        {
            InitializeComponent();
            mainContentControl.Content = new AdminHomeControl();
        }

        public void ConsultationUtilisateursControl()
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var consultationAbonnesControl = container.Resolve<ConsultationAbonnesControl>();

            mainContentControl.Content = consultationAbonnesControl;
        }

        public void ConsultationFilmsControls()
        {
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var consultationFilmsControl = container.Resolve<ConsultationFilmsControl>();
            mainContentControl.Content = consultationFilmsControl;
        }
    }
}