namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class FilmAlreadyExistInFilmsList : Exception
    {
        public FilmAlreadyExistInFilmsList()
        {
        }

        public FilmAlreadyExistInFilmsList(string message) : base(message)
        {
        }

        public FilmAlreadyExistInFilmsList(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}