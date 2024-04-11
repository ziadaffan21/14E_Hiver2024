using MongoDB.Driver;

namespace CineQuebec.Windows.DAL
{
    public interface IDataBaseUtils
    {
        IMongoDatabase ConnectDatabase(IMongoClient mongoDBClient);

        IMongoClient OuvrirConnexion();
    }
}