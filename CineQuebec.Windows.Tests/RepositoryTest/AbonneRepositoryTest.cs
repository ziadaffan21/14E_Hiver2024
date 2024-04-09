using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.Tests.RepositoryTest
{
    public class AbonneRepositoryTest
    {
        private AbonneRepository _abonneRepository;
        public AbonneRepositoryTest() { 
            var dataBaseMock=new Mock<IDataBaseUtils>();
            var MongoClient =new Mock<IMongoClient>();
            _abonneRepository=new AbonneRepository(dataBaseMock.Object,MongoClient.Object);
        }
    }
}
