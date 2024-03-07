using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public DateTime DateAdhesion { get; set; }
    }
}
