using System.Runtime.Serialization;

namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class RealisateurAlreadyExistInRealisateurList : Exception
    {
        public RealisateurAlreadyExistInRealisateurList()
        {
        }

        public RealisateurAlreadyExistInRealisateurList(string? message) : base(message)
        {
        }

        public RealisateurAlreadyExistInRealisateurList(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RealisateurAlreadyExistInRealisateurList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}