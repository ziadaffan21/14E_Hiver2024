namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IActeur
    {
        DateTime Naissance { get; set; }
        string Nom { get; set; }
        string Prenom { get; set; }

        string ToString();
    }
}