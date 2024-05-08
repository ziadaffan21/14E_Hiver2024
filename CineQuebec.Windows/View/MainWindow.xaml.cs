using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View;
using System.Windows;
using Unity;

namespace CineQuebec.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var container = (IUnityContainer)Application.Current.Resources["UnityContainer"];
            var connexionControl = container.Resolve<ConnexionControl>();

            mainContentControl.Content = connexionControl;
        }

        public void ConnecterWindow(Abonne abonne)
        {
            ConnecteWindowPrincipal connecteWindowPrincipal = new ConnecteWindowPrincipal(abonne);
            Application.Current.MainWindow = connecteWindowPrincipal;

            connecteWindowPrincipal.Show();
            Close();
        }
    }
}