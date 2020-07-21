using Crochet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IProductPictureService
    {
        Task<IList<ProductPicture>> GetPicturesByProductId(int id);

        Task<ProductPicture> GetPictureById(int id);

        Stream GetPictureById(string id);

        Task<ProductPicture> UpsertPicture(ProductPicture picture, Stream pictureStream);

        Task<ProductPicture> DeletePicture(ProductPicture picture);
    }
}
