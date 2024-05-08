namespace CineQuebec.Windows.DAL.Services
{
    [Serializable]
    public class RealisateurAlreadyExistInRealisateursList : Exception
    {
        public RealisateurAlreadyExistInRealisateursList()
        {
        }

        public RealisateurAlreadyExistInRealisateursList(string message) : base(message)
        {
        }

        public RealisateurAlreadyExistInRealisateursList(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}