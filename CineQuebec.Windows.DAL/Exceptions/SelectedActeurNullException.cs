using System.Runtime.Serialization;

namespace CineQuebec.Windows.ViewModel
{
    [Serializable]
    public class SelectedActeurNullException : Exception
    {
        public SelectedActeurNullException()
        {
        }

        public SelectedActeurNullException(string message) : base(message)
        {
        }

        public SelectedActeurNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelectedActeurNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}