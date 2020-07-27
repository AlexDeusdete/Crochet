using System.ComponentModel.DataAnnotations.Schema;

namespace Crochet.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public int? ProductTypeId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Difficulty { get; set; }
        public string Comments { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
