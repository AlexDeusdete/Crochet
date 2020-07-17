using Crochet.Interfaces;
using Crochet.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crochet.Services.LiteDB
{
    public class ProductPictureService : LiteDBBase, IProductPictureService
    {
        private readonly ILiteCollection<ProductPicture> _liteCollection;
        public ProductPictureService()
        {
            var dataBase = GetDBInstance();
            _liteCollection = dataBase.GetCollection<ProductPicture>();
        }
        public Task DeletePicture(ProductPicture picture)
        {
            if(picture != null && picture.Name != null)
                GetDBInstance().FileStorage.Delete(picture.Name);
            return Task.FromResult(
                _liteCollection.Delete(picture.Id)
                );
        }
        public Task<ProductPicture> GetPictureById(int id)
        {
            return Task.FromResult(_liteCollection.FindById(id));
        }

        public Stream GetPictureById(string id)
        {
            try
            {
                return GetDBInstance().FileStorage.OpenRead(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<ProductPicture> GetPictureByName(string Name)
        {
            return Task.FromResult(_liteCollection.FindOne(x => x.Name == Name));
        }
        public async Task<IList<ProductPicture>> GetPicturesByProductId(int id)
        {
            return await Task.FromResult(_liteCollection
                                                .Query()
                                                .Where(x => x.ProductId == id).ToList());
        }
        public void UpsertPicture(ProductPicture picture, Stream pictureStream)
        {
            _liteCollection.Insert(picture);
            ProductPicture updatedPicture = _liteCollection.Query().OrderByDescending(x => x.Id).FirstOrDefault();

            string imgName = "IMG" + updatedPicture.Id.ToString();
            GetDBInstance().FileStorage.Upload(imgName, imgName, pictureStream);

            updatedPicture.Name = imgName;
            _liteCollection.Update(updatedPicture);
        }
    }
}
