using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IPictureGalleryService
    {
        Task<Stream> GetPictureStreamAsync();

        Task<Stream> TakePicture();
    }
}
