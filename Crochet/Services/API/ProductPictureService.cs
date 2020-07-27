using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class ProductPictureService : ApiBase, IProductPictureService
    {
        private readonly IPictureService _pictureService;
        public ProductPictureService(IApi api,
                                     IPictureService pictureService) : base(api)
        {
            _pictureService = pictureService;
        }
        public async Task<ProductPicture> DeletePicture(ProductPicture picture)
        {
   
            if (picture != null && picture.Name != null)
                await _pictureService.DeleteImage(picture.Name);

            return await API.DeleteProductPicture(picture.Id);
        }

        public async Task<ProductPicture> GetPictureById(int id)
        {
            return await API.GetProductPicture(id);
        }

        public Stream GetPictureById(string id)
        {
            throw new NotImplementedException();
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

            string imgName = "IMG" + updatedPicture.Id.ToString() + ".png";
            updatedPicture.Uri = await _pictureService.UploadImage(imgName, pictureStream);

            updatedPicture.Name = imgName;
            return await API.PutProductPicture(updatedPicture.Id, updatedPicture);
        }
    }
}
