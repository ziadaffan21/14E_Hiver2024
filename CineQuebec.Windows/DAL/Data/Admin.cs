using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Admin : Abonne
    {
		private bool _isAdmin;

        public bool IsAdmin
		{
			get { return _isAdmin; }
			set { _isAdmin = value; }
		}
        public Admin(string? username) : base(username)
        {
        }
    }
}
