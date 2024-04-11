using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour AdminHomeControl.xaml
    /// </summary>
    public partial class AdminHomeControl : UserControl
    {
        public AdminHomeControl()
        {
            InitializeComponent();
        }

        private void Button_Click_Utilisateurs(object sender, RoutedEventArgs e)
        {
            ((ConnecteWindowPrincipal)Application.Current.MainWindow).ConsultationUtilisateursControl();
        }

        private void Button_Click_Films(object sender, RoutedEventArgs e)
        {
            ((ConnecteWindowPrincipal)Application.Current.MainWindow).ConsultationFilmsControls();
        }
    }
}