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

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NoterView.xaml
    /// </summary>
    public partial class NoterView : Window
    {
        public NoterView()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO : Mettre le code d'envoie et de validation.
            MessageBox.Show("Merci pour votre évaluation.", "Confirmation d'envoie", MessageBoxButton.OK);
            Close();
        }
    }
}
