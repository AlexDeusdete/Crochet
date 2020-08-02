using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Thumbnails
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("samples-workitems/{name}", Connection = "DefaultEndpointsProtocol=https;AccountName=crochet;AccountKey=oqajzpu+Sl8oMWk7F1PoichrBWe+t8ddbA08dBbquLSe9kXFkfRjCPsRhy5ZPQFigInxAbunh47cLF59bxKr8Q==;EndpointSuffix=core.windows.net")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
