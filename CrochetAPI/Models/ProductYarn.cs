namespace Crochet.Models
{
    public class ProductYarn
    {
        public int Id { get; set; }
        public int YarnId { get; set; }
        public int ProductId { get; set; }
        public float Cost { get; set; }
        public float Consumption { get; set; }
        public virtual Yarn Yarn { get; set; }
        public int VariationId { get; set; }
        public string VariationName { get; set; }
    }
}
