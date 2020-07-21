using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductFinalcialService : ApiBase, IProductFinalcialService
    {
        public ProductFinalcialService(IApi api):base(api){}
        public async Task<ProductFinalcial> GetFinalcialByVariationId(int productId, int variationId)
        {
            return await Task.FromResult(
                                            (await GetFinalcialsByProductId(productId))
                                            .Where(x => x.VariationId == variationId)
                                            .FirstOrDefault()
                                        );
        }

        public async Task<IList<ProductFinalcial>> GetFinalcialsByProductId(int productId)
        {
            return await API.GetProductFinalcialByProductId(productId);
        }

        public async Task<ProductFinalcial> UpSertItem(ProductFinalcial productFinalcial)
        {
            if(productFinalcial.Id > 0)
                return await API.PutProductFinalcial(productFinalcial.Id, productFinalcial);
            else
                return await API.PostProductFinalcial(productFinalcial);
        }
    }
}
