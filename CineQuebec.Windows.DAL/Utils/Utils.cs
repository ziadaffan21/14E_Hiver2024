namespace CineQuebec.Windows.DAL.Utils
{
    public static class Utils
    {
        public static string GetMoisNom(DateTime date)
        {
            return date.ToString("MMMM", new System.Globalization.CultureInfo("fr-FR"));
        }
    }
}