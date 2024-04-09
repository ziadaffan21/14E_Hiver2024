using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
