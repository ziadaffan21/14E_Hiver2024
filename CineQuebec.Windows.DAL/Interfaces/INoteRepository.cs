﻿using CineQuebec.Windows.DAL.Entities;
using MongoDB.Bson;

namespace CineQuebec.Windows.BLL.Tests
{
    public interface INoteRepository
    {
        Task<Note> Add(Note note);
        Task<bool> Delete(Note note);
        Task<Note> FindById(ObjectId id);
        Task<List<Note>> GetAll();
        Task<Note> Update(Note note);
    }
}