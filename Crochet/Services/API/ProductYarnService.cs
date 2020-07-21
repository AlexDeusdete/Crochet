using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductYarnService : ApiBase, IProductYarnService
    {
        public ProductYarnService(IApi api):base(api){}

        public async Task<IList<ProductYarnGroup>> GetProductYarnsGroup(int productId)
        {
            var yarnGroups = new List<ProductYarnGroup>();
            var yarnItems = await GetYarns(productId);

            foreach (var items in yarnItems
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
            return await API.GetProductYarnsByProductId(productId);
        }

        public async Task<ProductYarn> UpsertItem(ProductYarn productYarn)
        {
            if (productYarn.Id > 0)
                return await API.PutProductYarn(productYarn.Id, productYarn);
            else
                return await API.PostProductYarn(productYarn);
        }
    }
}
