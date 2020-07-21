using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Services.LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductPictureService : ApiBase, IProductPictureService
    {
        public ProductPictureService(IApi api) : base(api){}
        public async Task<ProductPicture> DeletePicture(ProductPicture picture)
        {
            LiteDBBase dBBase = new LiteDBBase();
            if (picture != null && picture.Name != null)
                dBBase.GetDBInstance().FileStorage.Delete(picture.Name);

            return await API.DeleteProductPicture(picture.Id);
        }

        public async Task<ProductPicture> GetPictureById(int id)
        {
            return await API.GetProductPicture(id);
        }

        public Stream GetPictureById(string id)
        {
            try
            {
                LiteDBBase dBBase = new LiteDBBase();
                return dBBase.GetDBInstance().FileStorage.OpenRead(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<ProductPicture>> GetPicturesByProductId(int id)
        {
            return await API.GetProductPicturesByProduct(id);
        }

        public async Task<ProductPicture> UpsertPicture(ProductPicture picture, Stream pictureStream)
        {
            ProductPicture updatedPicture;
            if (picture.Id > 0)
                updatedPicture = await API.PutProductPicture(picture.Id, picture);
            else
                updatedPicture = await API.PostProductPicture(picture);

            LiteDBBase dBBase = new LiteDBBase();

            string imgName = "IMG" + updatedPicture.Id.ToString();
            dBBase.GetDBInstance().FileStorage.Upload(imgName, imgName, pictureStream);

            updatedPicture.Name = imgName;
            return await API.PutProductPicture(updatedPicture.Id, updatedPicture);
        }
    }
}
