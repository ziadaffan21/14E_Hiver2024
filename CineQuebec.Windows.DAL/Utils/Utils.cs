using System.Runtime.InteropServices;
using System.Security;

namespace CineQuebec.Windows.DAL.Utils
{
    public static class Utils
    {
        public static string GetMoisNom(DateTime date)
        {
            return date.ToString("MMMM", new System.Globalization.CultureInfo("fr-FR"));
        }

        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException(nameof(securePassword));

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}