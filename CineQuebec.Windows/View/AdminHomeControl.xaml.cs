using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
