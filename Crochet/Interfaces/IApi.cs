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

        [Get("/Products?productTypeId={productTypeId}")]
        Task<List<Product>> GetProductsByType(int productTypeId);

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

        #region ProductType
        [Get("/ProductTypes")]
        Task<List<ProductType>> GetProductType();

        [Get("/ProductTypes/{id}")]
        Task<ProductType> GetProductType(int id);

        [Post("/ProductTypes")]
        Task<ProductType> PostProductType([Body] ProductType productType);
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

        #region Sale
        [Get("/Sales?status={status}&finalized={finalized}")]
        Task<List<Sale>> GetSales(int? status, bool? finalized);

        [Get("/Sales/{id}")]
        Task<Sale> GetSale(int id);

        [Post("/Sales")]
        Task<Sale> PostSale([AliasAs("Sale")] [Body] Sale sale);

        [Put("/Sales/{id}")]
        Task<Sale> PutSale(int id, [Body] Sale sale);

        [Delete("/Sales/{id}")]
        Task<Sale> DeleteSale(int id);
        #endregion

        #region SaleItem
        [Get("/SaleItems?sale={sale}")]
        Task<List<SaleItem>> GetSaleItems(int? sale);

        [Get("/SaleItems/{id}")]
        Task<SaleItem> GetSaleItem(int id);

        [Post("/SaleItems")]
        Task<SaleItem> PostSaleItems([AliasAs("SaleItem")] [Body] SaleItem saleItem);

        [Put("/SaleItems/{id}")]
        Task<SaleItem> PutSaleItem(int id, [Body] SaleItem saleItem);

        [Delete("/SaleItems/{id}")]
        Task<SaleItem> DeleteSaleItem(int id);
        #endregion

        #region Customer
        [Get("/Customers")]
        Task<List<Customer>> GetCustomers();

        [Get("/Customers/{id}")]
        Task<Customer> GetCustomer(int id);

        [Post("/Customers")]
        Task<Customer> PostCustomer([Body] Customer customer);
        [Put("/Customers/{id}")]
        Task<Customer> PutCustomer(int id, [Body] Customer customer);
        #endregion
    }
}
