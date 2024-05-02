using System.Runtime.Serialization;

namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class CategorieAlreadyExistInCategoriesList : Exception
    {
        public CategorieAlreadyExistInCategoriesList()
        {
        }

        public CategorieAlreadyExistInCategoriesList(string? message) : base(message)
        {
        }

        public CategorieAlreadyExistInCategoriesList(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CategorieAlreadyExistInCategoriesList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}