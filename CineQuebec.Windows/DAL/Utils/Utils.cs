using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    internal class Utils
    {
        public static string GetMoisNom(DateTime date)
        {
            return date.ToString("MMMM", new System.Globalization.CultureInfo("fr-FR"));
        }
    }
}
