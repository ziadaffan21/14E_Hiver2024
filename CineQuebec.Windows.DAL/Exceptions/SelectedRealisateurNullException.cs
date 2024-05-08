using System.Runtime.Serialization;

namespace CineQuebec.Windows.ViewModel
{
    [Serializable]
    public class SelectedRealisateurNullException : Exception
    {
        private string selection_un_realisateur_ajout;
        private string v;

        public SelectedRealisateurNullException()
        {
        }

        public SelectedRealisateurNullException(string message) : base(message)
        {
        }

        public SelectedRealisateurNullException(string selection_un_realisateur_ajout, string v)
        {
            this.selection_un_realisateur_ajout = selection_un_realisateur_ajout;
            this.v = v;
        }

        public SelectedRealisateurNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelectedRealisateurNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}