using Ambev.Core.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Infrastructure.Persistence.MongoDB.Configuration
{
    public class MongoDbInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoDbInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task InitializeAsync()
        {
            // Verifica se a coleção "Carts" já existe
            var collections = await _database.ListCollectionNamesAsync();
            var collectionList = await collections.ToListAsync();

            if (!collectionList.Contains("Carts"))
            {
                // Cria a coleção "Carts"
                await _database.CreateCollectionAsync("Carts");

                // Cria um índice no campo UserId
                var cartsCollection = _database.GetCollection<Cart>("Carts");
                var indexKeys = Builders<Cart>.IndexKeys.Ascending(c => c.UserId);
                await cartsCollection.Indexes.CreateOneAsync(new CreateIndexModel<Cart>(indexKeys));
            }
        }
    }
}
