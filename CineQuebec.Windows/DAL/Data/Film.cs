using CineQuebec.Windows.DAL.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film : Entity
    {
		private string? _name;

		public string? Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private Categories _categorie;

		public Categories Categorie
		{
			get { return _categorie; }
			set { _categorie = value; }
		}

        public Film(string? name, Categories categorie)
        {
            Name = name;
            Categorie = categorie;
        }
        public override string ToString()
        {
            return $"${Name}";
        }
    }
}
