using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Crochet.Models;
using Refit;
namespace Crochet.Interfaces
{
    public interface IApi
    {
        #region Brand
        [Get("/Brands")]
        Task<List<Brand>> GetBrands();

        [Get("/Brands/{id}")]
        Task<Brand> GetBrand(int id);

        [Post("/Brands")]
        Task<Brand> PostBrand([Body] Brand brand);
        #endregion

        #region Product
        [Get("/Products?name={name}")]
        Task<List<Product>> GetProducts(string name);

        [Get("/Products/{id}")]
        Task<Product> GetProduct(int id);

        [Post("/Products")]
        Task<Product> PostProduct([Body] Product product);

        [Put("/Products/{id}")]
        Task<Product> PutProduct(int id, [Body] Product product);
        #endregion

        #region ProductFinalcial
        [Get("/ProductFinalcials")]
        Task<List<ProductFinalcial>> GetProductFinalcials();

        [Get("/ProductFinalcials/ProductId/{id}")]
        Task<List<ProductFinalcial>> GetProductFinalcialByProductId(int id);

        [Get("/ProductFinalcials/{id}")]
        Task<ProductFinalcial> GetProductFinalcial(int id);

        [Post("/ProductFinalcials")]
        Task<ProductFinalcial> PostProductFinalcial([Body] ProductFinalcial product);

        [Put("/ProductFinalcials/{id}")]
        Task<ProductFinalcial> PutProductFinalcial(int id, [Body] ProductFinalcial product);
        #endregion

        #region ProductYarns
        [Get("/ProductYarns")]
        Task<List<ProductYarn>> GetProductYarns();

        [Get("/ProductYarns/{id}")]
        Task<ProductYarn> GetProductYarn(int id);

        [Get("/ProductYarns/ProductId/{id}")]
        Task<List<ProductYarn>> GetProductYarnsByProductId(int id);

        [Post("/ProductYarns")]
        Task<ProductYarn> PostProductYarn([Body] ProductYarn product);

        [Put("/ProductYarns/{id}")]
        Task<ProductYarn> PutProductYarn(int id, [Body] ProductYarn product);
        [Delete("/ProductYarns/{id}")]
        Task<ProductYarn> DeleteProductYarn(int id);
        #endregion

        #region ProductPictures
        [Get("/ProductPictures")]
        Task<List<ProductPicture>> GetProductPictures();

        [Get("/ProductPictures/{id}")]
        Task<ProductPicture> GetProductPicture(int id);

        [Get("/ProductPictures/ProductId/{id}")]
        Task<List<ProductPicture>> GetProductPicturesByProduct(int id);

        [Post("/ProductPictures")]
        Task<ProductPicture> PostProductPicture([Body] ProductPicture product);

        [Put("/ProductPictures/{id}")]
        Task<ProductPicture> PutProductPicture(int id, [Body] ProductPicture product);

        [Delete("/ProductPictures/{id}")]
        Task<ProductPicture> DeleteProductPicture(int id);
        #endregion

        #region Yarn
        [Get("/Yarns")]
        Task<List<FeedStock>> GetYarns();

        [Get("/Yarns/{id}")]
        Task<FeedStock> GetYarn(int id);

        [Post("/Yarns")]
        Task<FeedStock> PostYarn([AliasAs("Yarn")] [Body] FeedStock product);

        [Put("/Yarns/{id}")]
        Task<FeedStock> PutYarn(int id, [Body] FeedStock yarn);

        [Delete("/Yarns/{id}")]
        Task<FeedStock> DeleteYarn(int id);
        #endregion
    }
}
