using System.Runtime.Serialization;

namespace CineQuebec.Windows.Exceptions.AbonneExceptions
{
    [Serializable]
    public class NumberActeursOutOfRange : Exception
    {
        public NumberActeursOutOfRange()
        {
        }

        public NumberActeursOutOfRange(string? message) : base(message)
        {
        }

        public NumberActeursOutOfRange(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NumberActeursOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}