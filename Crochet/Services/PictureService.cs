using Azure.Storage.Blobs;
using Crochet.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services
{
    public class PictureService : IPictureService
    {
        private string ACS = "DefaultEndpointsProtocol=https;AccountName=crochet;AccountKey=Qi+YSmigiEm6G2p6kMptS0HRNGppLM26kmNigaMswwis7BbQOhNNmc0pRchMOjQ1+2swHFcB0gptmZ5SRF4EZA==;EndpointSuffix=core.windows.net";
        private BlobServiceClient _client;
        private BlobContainerClient _containerClient;
        private BlobClient _blobClient;

        public PictureService()
        {
            _client = new BlobServiceClient(ACS);
            _containerClient = _client.GetBlobContainerClient("pictures");
        }
        public Task<string> UploadImage(MemoryStream image)
        {
            using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));

            await containerClient.UploadBlobAsync(fileName, memoryStream);

            resultsLabel.Text += "Blob Uploaded\n";
        }
    }
}
