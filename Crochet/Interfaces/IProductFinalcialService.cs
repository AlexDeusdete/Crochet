using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IProductFinalcialService
    {
        Task<IList<ProductFinalcial>> GetFinalcialsByProductId(int productId);
        Task<ProductFinalcial> GetFinalcialByVariationId(int productId, int variationId);
        Task<ProductFinalcial> UpSertItem(ProductFinalcial productFinalcial);
    }
}
