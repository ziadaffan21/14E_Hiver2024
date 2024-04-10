namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IAbonne
    {
        DateTime DateAdhesion { get; set; }
        byte[] Password { get; set; }
        string Username { get; set; }

        string ToString();
    }
}