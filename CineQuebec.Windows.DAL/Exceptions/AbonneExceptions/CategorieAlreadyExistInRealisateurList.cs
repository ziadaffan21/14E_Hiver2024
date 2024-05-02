using System.Runtime.Serialization;

namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class CategorieAlreadyExistInRealisateurList : Exception
    {
        public CategorieAlreadyExistInRealisateurList()
        {
        }

        public CategorieAlreadyExistInRealisateurList(string? message) : base(message)
        {
        }

        public CategorieAlreadyExistInRealisateurList(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CategorieAlreadyExistInRealisateurList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}