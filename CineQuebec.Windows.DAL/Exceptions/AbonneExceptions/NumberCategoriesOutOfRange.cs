namespace CineQuebec.Windows.Exceptions.AbonneExceptions
{
    [Serializable]
    public class NumberCategoriesOutOfRange : Exception
    {
        public NumberCategoriesOutOfRange()
        {
        }

        public NumberCategoriesOutOfRange(string message) : base(message)
        {
        }

        public NumberCategoriesOutOfRange(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}