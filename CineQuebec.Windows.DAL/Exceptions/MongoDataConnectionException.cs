namespace CineQuebec.Windows.Exceptions
{
    public class MongoDataConnectionException : Exception
    {
        public MongoDataConnectionException(string message) : base(message)
        {
        }
    }
}