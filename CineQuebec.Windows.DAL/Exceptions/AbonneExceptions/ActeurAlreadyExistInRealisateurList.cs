using System.Runtime.Serialization;

namespace CineQuebec.Windows.Exceptions.AbonneExceptions
{
    [Serializable]
    public class ActeurAlreadyExistInRealisateurList : Exception
    {
        public ActeurAlreadyExistInRealisateurList()
        {
        }

        public ActeurAlreadyExistInRealisateurList(string? message) : base(message)
        {
        }

        public ActeurAlreadyExistInRealisateurList(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ActeurAlreadyExistInRealisateurList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}