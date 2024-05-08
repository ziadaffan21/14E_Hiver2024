namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class CategorieAlreadyExistInCategoriesList : Exception
    {
        public CategorieAlreadyExistInCategoriesList()
        {
        }

        public CategorieAlreadyExistInCategoriesList(string message) : base(message)
        {
        }

        public CategorieAlreadyExistInCategoriesList(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}