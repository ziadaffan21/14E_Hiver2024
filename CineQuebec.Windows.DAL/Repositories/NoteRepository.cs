using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Tests
{
    public class NoteRepository : INoteRepository
    {
        private readonly IMongoClient _mongoDBClient;
        private readonly IMongoDatabase _database;
        private readonly IDataBaseUtils _dataBaseUtils;
        private const string NOTES = "Notes";
        private IMongoCollection<Note> collection;

        public NoteRepository(IDataBaseUtils dataBaseUtils, IMongoClient mongoDBClient = null)
        {
            _dataBaseUtils = dataBaseUtils;
            _mongoDBClient = mongoDBClient ?? _dataBaseUtils.OuvrirConnexion();
            _database = _dataBaseUtils.ConnectDatabase(_mongoDBClient);
            collection = _database.GetCollection<Note>(NOTES);
        }

        public Task<List<Note>> GetAll()
        {
            return collection.Aggregate().ToListAsync();
        }

        public Task<Note> Add(Note note)
        {
            return (Task<Note>)collection.InsertOneAsync(note);
        }

        public async Task<bool> Delete(Note note)
        {
            var deleteResult = await collection.DeleteOneAsync(Builders<Note>.Filter.Eq(n => n.Id, note.Id));
            return deleteResult.DeletedCount > 0;
        }


        public async Task<Note> Update(Note note)
        {
            var existingNote = await collection.Find(Builders<Note>.Filter.Eq(n => n.Id, note.Id)).FirstOrDefaultAsync();

            if (existingNote != null)
            {
                existingNote = note;

                var updateResult = await collection.UpdateOneAsync(
                    Builders<Note>.Filter.Eq(n => n.Id, note.Id),
                    Builders<Note>.Update.Set(n => n, existingNote)
                );

                if (updateResult.ModifiedCount > 0)
                {
                    return existingNote;
                }
            }

            return null;
        }

        public async Task<Note> FindById(ObjectId id)
        {
            return await collection.Find(Builders<Note>.Filter.Eq(n => n.Id, id)).FirstOrDefaultAsync();
        }
    }
}
