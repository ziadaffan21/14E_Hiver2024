using System.Runtime.Serialization;

namespace CineQuebec.Windows.BLL.Tests
{
    [Serializable]
    public class CouldNotFoundNoteException : Exception
    {
        public CouldNotFoundNoteException()
        {
        }

        public CouldNotFoundNoteException(string message) : base(message)
        {
        }

        public CouldNotFoundNoteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouldNotFoundNoteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}