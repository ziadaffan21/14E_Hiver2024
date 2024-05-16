using CineQuebec.Windows.DAL.Data;
using System.Windows;
using Unity;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConnecteWindowPrincipal.xaml
    /// </summary>
    public partial class ConnecteWindowPrincipal : Window
    {
        public Abonne User { get; set; }
        public ConnecteWindowPrincipal(Abonne abonne = null)
        {
            InitializeComponent();
            User = abonne;
            if ((bool)abonne.IsAdmin)
            {
                var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
                var adminHomeControl = container.Resolve<AdminHomeControl>();
                mainContentControl.Content = adminHomeControl;
            }
            else
            {
                var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
                var abonneHomeControl = container.Resolve<AbonneHomeControl>();
                abonneHomeControl.setUser(abonne);
                mainContentControl.Content = abonneHomeControl;
            }
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
            var consultationFilmsControl = container.Resolve<ConsultationFilmsProjectionsControl>();
            mainContentControl.Content = consultationFilmsControl;
        }
    }
}