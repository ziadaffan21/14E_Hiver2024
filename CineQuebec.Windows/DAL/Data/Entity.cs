using CineQuebec.Windows.Exceptions.EntitysExceptions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Entity
    {
        #region ATTRIBUTS
        private ObjectId _id;
        #endregion

        #region PROPRIÉTÉS ET INDEXEURS
        public ObjectId Id
        {
            get { return _id; }
            set
            {
                if (!ObjectId.TryParse(value.ToString(), out _)) throw new InvalidGuidException($"L'id {Id} est invalid");
                _id = value;
            }
        }
        #endregion
    }
}
