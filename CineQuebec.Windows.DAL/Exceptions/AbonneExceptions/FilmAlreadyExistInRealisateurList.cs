using System.Runtime.Serialization;

namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class FilmAlreadyExistInRealisateurList : Exception
    {
        public FilmAlreadyExistInRealisateurList()
        {
        }

        public FilmAlreadyExistInRealisateurList(string? message) : base(message)
        {
        }

        public FilmAlreadyExistInRealisateurList(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FilmAlreadyExistInRealisateurList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}