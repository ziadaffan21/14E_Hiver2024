using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Entities
{
    public class Note : Entity
    {
        private ObjectId _filmId;

        public ObjectId FilmId
        {
            get { return _filmId; }
            set { _filmId = value; }
        }

        private ObjectId _abonneId;

        public ObjectId AbonneId
        {
            get { return _abonneId; }
            set { _abonneId = value; }
        }
        private int _noteValue;

        public int NoteValue
        {
            get { return _noteValue; }
            set { _noteValue = value; }
        }

        public Note()
        {
        }
        public Note(ObjectId filmId, ObjectId abonneId, int noteValue)
        {
            FilmId = filmId;
            AbonneId = abonneId;
            NoteValue = noteValue;
        }
    }
}
