using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IPictureService
    {
        Task<string> UploadImage(string fileName, Stream image);
        Task<bool> DeleteImage(string fileName);
    }
}
