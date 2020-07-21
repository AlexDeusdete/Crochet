using Crochet.Interfaces;
using Crochet.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.LiteDB
{
    public class ProductService : LiteDBBase, IProductService
    {
        private readonly ILiteCollection<Product> _liteCollection;

        public ProductService()
        {
            var dataBase = GetDBInstance();
            _liteCollection = dataBase.GetCollection<Product>();
        }

        public async Task<Product> GetItemByName(string Name)
        {
            return await Task.FromResult(_liteCollection
                                                .Query()
                                                .Where(x => x.Name == Name)
                                                .FirstOrDefault());
        }

        public async Task<IList<Product>> GetItems()
        {
            return await Task.FromResult(_liteCollection.FindAll().ToList());
        }

        public int NextId()
        {
            return _liteCollection.FindAll().Max(x => x.Id) + 1;
        }

        public Task<Product> UpsertItem(Product Item)
        {
            _liteCollection.Upsert(Item);
            return null;
        }
    }
}
