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
    public class ProductYarnService : LiteDBBase, IProductYarnService
    {
        private readonly ILiteCollection<ProductYarn> _liteCollection;

        public ProductYarnService()
        {
            var database = GetDBInstance();
            _liteCollection = database.GetCollection<ProductYarn>();
        }
        public async Task<IList<ProductYarnGroup>> GetProductYarnsGroup(int productId)
        {
            var yarnGroups = new List<ProductYarnGroup>();
            var yarnItems = await GetYarns(productId);

            foreach( var items in yarnItems
                                    .GroupBy(x => x.VariationId)
                                    .Select(grp => grp.ToList())
                                    .ToList())
            {
                var yarnGroup = new ProductYarnGroup(items[0].VariationId, items[0].VariationName);

                var yarnCollection = new ProductYarnCollection(items[0].VariationId, items[0].VariationName);
                yarnCollection.AddRange(items);

                yarnGroup.Add(yarnCollection);

                yarnGroups.Add(yarnGroup);
            }

            return yarnGroups;
        }

        public async Task<IList<ProductYarn>> GetYarns(int productId)
        {
            return await Task.FromResult(_liteCollection                                            
                                            .Query()
                                            .Include(x => x.Yarn)
                                            .Include(x => x.Yarn.Brand)
                                            .Where(x => x.ProductId == productId)
                                            .ToList());
        }

        public Task<ProductYarn> UpsertItem(ProductYarn productYarn)
        {
            _liteCollection.Upsert(productYarn);
            return null;
        }
    }
}
