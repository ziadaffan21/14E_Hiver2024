using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View;
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

namespace CineQuebec.Windows.Controls
{
    /// <summary>
    /// Logique d'interaction pour FilmControl.xaml
    /// </summary>
    public partial class FilmControl : UserControl
    {
        public Film Film
        {
            get { return (Film)GetValue(FilmProperty); }
            set { SetValue(FilmProperty, value); }
        }

        public static readonly DependencyProperty FilmProperty =
            DependencyProperty.Register("Film", typeof(Film), typeof(FilmControl));

        public FilmControl()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OuvrirDetails();
        }

        private void OuvrirDetails()
        {
            FilmDetailsView filmDetailsView = new((Film)DataContext);
            filmDetailsView.Show();
        }
    }
}
