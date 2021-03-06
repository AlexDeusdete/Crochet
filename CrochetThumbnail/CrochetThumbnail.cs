// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System.IO;
using Azure.Storage.Blobs;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.Formats;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace CrochetThumbnail
{
    public static class CrochetThumbnail
    {
        private static string GetBlobNameFromUrl(string bloblUrl)
        {
            var uri = new Uri(bloblUrl);
            var blobClient = new BlobClient(uri);
            return blobClient.Name;
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;

            extension = extension.Replace(".", "");

            var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", RegexOptions.IgnoreCase);

            if (isSupported)
            {
                switch (extension)
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "gif":
                        encoder = new GifEncoder();
                        break;
                    default:
                        break;
                }
            }

            return encoder;
        }

        [FunctionName("CrochetThumbnail")]
        public static async Task Run([EventGridTrigger]EventGridEvent eventGridEvent,
                                [Blob("{data.url}", FileAccess.Read)] Stream input,
                                ILogger log)
        {
            try
            {
                if (input != null)
                {
                    var createdEvent = JObject.FromObject(eventGridEvent.Data).ToObject<StorageBlobCreatedEventData>();
                    var extension = Path.GetExtension(createdEvent.Url);
                    var encoder = GetEncoder(extension);

                    if (encoder != null)
                    {
                        var thumbnailWidth = Convert.ToInt32(Environment.GetEnvironmentVariable("THUMBNAIL_WIDTH"));
                        var thumbContainerName = Environment.GetEnvironmentVariable("THUMBNAIL_CONTAINER_NAME");
                        var blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("BLOB_STORAGE_CONNECTION_STRING"));
                        var blobContainerClient = blobServiceClient.GetBlobContainerClient(thumbContainerName);
                        var blobName = GetBlobNameFromUrl(createdEvent.Url);

                        using (var output = new MemoryStream())
                        using (Image<Rgba32> image = Image.Load<Rgba32>(input))
                        {
                            var divisor = image.Width / thumbnailWidth;
                            var height = Convert.ToInt32(Math.Round((decimal)(image.Height / divisor)));

                            image.Mutate(x => x.Resize(thumbnailWidth, height));
                            image.Save(output, encoder);
                            output.Position = 0;
                            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
                            await blobClient.UploadAsync(output, new BlobHttpHeaders { ContentType = "image/png" });
                        }
                    }
                    else
                    {
                        log.LogInformation($"No encoder support for: {createdEvent.Url}");
                    }                    
                }
            }
            catch (Exception ex)
            {
                log.LogInformation("ERROR : "+ex.Message);
                throw;
            }
        }
    }
}
