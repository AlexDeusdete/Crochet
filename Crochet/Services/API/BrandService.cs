using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class BrandService : ApiBase, IBrandService
    {
        public BrandService(IApi api) : base(api){}
        public async Task<IList<Brand>> GetItems()
        {
            return await API.GetBrands();
        }

        public async Task<Brand> PutItem(Brand Item)
        {
            return await API.PostBrand(Item);
        }
    }
}
