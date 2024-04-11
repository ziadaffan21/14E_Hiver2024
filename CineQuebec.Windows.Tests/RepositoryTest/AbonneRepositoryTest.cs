using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Driver;
using Moq;

namespace CineQuebec.Windows.Tests.RepositoryTest
{
    public class AbonneRepositoryTest
    {
        private AbonneRepository _abonneRepository;

        public AbonneRepositoryTest()
        {
            var dataBaseMock = new Mock<IDataBaseUtils>();
            var MongoClient = new Mock<IMongoClient>();
            _abonneRepository = new AbonneRepository(dataBaseMock.Object, MongoClient.Object);
        }
    }
}