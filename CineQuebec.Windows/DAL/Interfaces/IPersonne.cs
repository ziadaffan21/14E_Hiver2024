namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IPersonne
    {
        DateTime Naissance { get; set; }
        string Nom { get; set; }
        string Prenom { get; set; }
    }
}