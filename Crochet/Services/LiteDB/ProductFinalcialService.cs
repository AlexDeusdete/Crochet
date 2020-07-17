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
    public class ProductFinalcialService : LiteDBBase, IProductFinalcialService
    {
        private readonly ILiteCollection<ProductFinalcial> _liteCollection;

        public ProductFinalcialService()
        {
            var database = GetDBInstance();
            _liteCollection = database.GetCollection<ProductFinalcial>();
        }
        public async Task<ProductFinalcial> GetFinalcialByVariationId(int productId, int variationId)
        {
            return await Task.FromResult(_liteCollection
                                            .Query()
                                            .Where(x => x.ProductId == productId && x.VariationId == variationId)
                                            .FirstOrDefault());
        }

        public async Task<IList<ProductFinalcial>> GetFinalcialsByProductId(int productId)
        {
            return await Task.FromResult(_liteCollection
                                            .Query()
                                            .Where(x => x.ProductId == productId)
                                            .ToList());
        }

        public void UpSertItem(ProductFinalcial productFinalcial)
        {
            _liteCollection.Upsert(productFinalcial);
        }
    }
}
