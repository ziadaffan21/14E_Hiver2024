using System.Runtime.Serialization;

namespace CineQuebec.Windows.ViewModel
{
    [Serializable]
    public class SelectedFilmNullException : Exception
    {
        public SelectedFilmNullException()
        {
        }

        public SelectedFilmNullException(string message) : base(message)
        {
        }

        public SelectedFilmNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelectedFilmNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}