namespace CineQuebec.Windows.Exceptions.AbonneExceptions
{
    [Serializable]
    public class ActeurAlreadyExistInActeursList : Exception
    {
        public ActeurAlreadyExistInActeursList()
        {
        }

        public ActeurAlreadyExistInActeursList(string message) : base(message)
        {
        }

        public ActeurAlreadyExistInActeursList(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}