using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Crochet.Models
{
    public class ProductPicture
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        [JsonIgnore]
        public string UriOriginalImage
        {
            get { return Uri?.Replace("thumbnails", "pictures"); } 
            set { } 
        }
    }
}
