using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Crochet.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services
{
    public class PictureService : IPictureService
    {
        private readonly string ACS = "DefaultEndpointsProtocol=https;AccountName=crochet;AccountKey=Qi+YSmigiEm6G2p6kMptS0HRNGppLM26kmNigaMswwis7BbQOhNNmc0pRchMOjQ1+2swHFcB0gptmZ5SRF4EZA==;EndpointSuffix=core.windows.net";
        private readonly BlobServiceClient _client;
        private readonly BlobContainerClient _containerClient;

        public PictureService()
        {
            _client = new BlobServiceClient(ACS);
            _containerClient = _client.GetBlobContainerClient("pictures");
        }

        public async Task<bool> DeleteImage(string fileName)
        {
            try
            {
                var result = await _containerClient.DeleteBlobAsync(fileName);
                return result.Status == 202;
            }
            catch (Exception Ex)
            {
                Crashes.TrackError(Ex);
                return false;
            }

            
        }

        public async Task<string> UploadImage(string fileName, Stream image)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            var result = await blobClient.UploadAsync(image, new BlobHttpHeaders { ContentType = "image/png" });

            if (result.GetRawResponse().Status == 201)
                return "https://crochet.blob.core.windows.net/pictures/" + fileName;
            else
                return "";
        }
    }
}
