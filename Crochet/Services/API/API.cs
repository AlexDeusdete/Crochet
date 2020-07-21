using Crochet.Interfaces;
using Crochet.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class API : IApi
    {
        private readonly IApi _api;
        public API()
        {
            _api = RestService.For<IApi>("https://crochet.azurewebsites.net/api");
        }

        public async Task<List<Brand>> GetBrands()
        {
            return await _api.GetBrands();
        }
        public async Task<Brand> GetBrand(int id)
        {
            return await _api.GetBrand(id);
        }
        public async Task<Brand> PostBrand([Body] Brand brand)
        {
            return await _api.PostBrand(brand);
        }

        public async Task<List<Product>> GetProducts(string name)
        {
            return await _api.GetProducts(name);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _api.GetProduct(id);
        }

        public async Task<Product> PostProduct([Body] Product product)
        {
            return await _api.PostProduct(product);
        }

        public async Task<Product> PutProduct(int id, [Body] Product product)
        {
            return await _api.PutProduct(id, product);
        }

        public async Task<List<ProductFinalcial>> GetProductFinalcials()
        {
            return await _api.GetProductFinalcials();
        }

        public async Task<List<ProductFinalcial>> GetProductFinalcialByProductId(int id)
        {
            return await _api.GetProductFinalcialByProductId(id);
        }

        public async Task<ProductFinalcial> GetProductFinalcial(int id)
        {
            return await _api.GetProductFinalcial(id);
        }

        public async Task<ProductFinalcial> PostProductFinalcial([Body] ProductFinalcial product)
        {
            return await _api.PostProductFinalcial(product);
        }

        public async Task<ProductFinalcial> PutProductFinalcial(int id, [Body] ProductFinalcial product)
        {
            return await _api.PutProductFinalcial(id, product);
        }

        public async Task<List<ProductYarn>> GetProductYarns()
        {
            return await _api.GetProductYarns();
        }

        public async Task<ProductYarn> GetProductYarn(int id)
        {
            return await _api.GetProductYarn(id);
        }

        public async Task<List<ProductYarn>> GetProductYarnsByProductId(int id)
        {
            return await _api.GetProductYarnsByProductId(id);
        }

        public async Task<ProductYarn> PostProductYarn([Body] ProductYarn product)
        {
            return await _api.PostProductYarn(product);
        }

        public async Task<ProductYarn> PutProductYarn(int id, [Body] ProductYarn product)
        {
            return await _api.PutProductYarn(id, product);
        }

        public async Task<List<ProductPicture>> GetProductPictures()
        {
            return await _api.GetProductPictures();
        }

        public async Task<ProductPicture> GetProductPicture(int id)
        {
            return await _api.GetProductPicture(id);
        }

        public async Task<List<ProductPicture>> GetProductPicturesByProduct(int id)
        {
            return await _api.GetProductPicturesByProduct(id);
        }

        public async Task<ProductPicture> PostProductPicture([Body] ProductPicture product)
        {
            return await _api.PostProductPicture(product);
        }

        public async Task<ProductPicture> PutProductPicture(int id, [Body] ProductPicture product)
        {
            return await _api.PutProductPicture(id, product);
        }

        public async Task<ProductPicture> DeleteProductPicture(int id)
        {
            return await _api.DeleteProductPicture(id);
        }

        public async Task<List<FeedStock>> GetYarns()
        {
            return await _api.GetYarns();
        }

        public async Task<FeedStock> GetYarn(int id)
        {
            return await _api.GetYarn(id);
        }

        public async Task<FeedStock> PostYarn([AliasAs("Yarn")] [Body] FeedStock product)
        {
            return await _api.PostYarn(product);
        }

        public async Task<FeedStock> PutYarn(int id, [Body] FeedStock product)
        {
            return await _api.PutYarn(id, product);
        }
    }
}
