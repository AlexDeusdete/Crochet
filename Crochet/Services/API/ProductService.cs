using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductService : ApiBase, IProductService
    {
        public ProductService(IApi api):base(api){}

        public async Task<IList<ProductGroup>> GetGroupItems()
        {
            var productGroups = new List<ProductGroup>();

            var productItems = await GetItems();

            foreach (var products in productItems
                                        .OrderByDescending(x => x.Id)
                                        .GroupBy(x => x.GroupName) 
                                        .Select(grp => grp.ToList())
                                        .ToList())
            {
                var productGroup = new ProductGroup(products[0].GroupName);

                var productCollection = new ProductCollection();
                productCollection.AddRange(products);
                productGroup.Add(productCollection);
                productGroups.Add(productGroup);
            }

            return productGroups;
        }

        public async Task<Product> GetItemByName(string Name)
        {
            var result = await API.GetProducts(Name);
            if (result != null && result.Count > 0)
                return result[0];
            return null;
        }

        public async Task<IList<Product>> GetItems()
        {
            return await API.GetProducts(null);
        }

        public async Task<IList<Product>> GetItemsByType(int productTypeid)
        {
            return await API.GetProductsByType(productTypeid);
        }

        public async Task<Product> UpsertItem(Product Item)
        {
            if (Item.Id > 0)
                return await API.PutProduct(Item.Id, Item);
            else
                return await API.PostProduct(Item);
        }
    }
}
