using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductTypeService : ApiBase, IProductTypeService
    {
        public ProductTypeService(IApi api):base(api){}

        public async Task<IList<ProductType>> GetItems()
        {
            return await API.GetProductType();
        }

        public async Task<ProductType> InsertItem(ProductType Item)
        {
            return await API.PostProductType(Item);
        }
    }
}
