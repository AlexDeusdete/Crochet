namespace Crochet.Models
{
    public class ProductFinalcial
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public string VariationName { get; set; }
        public float YarnsCost { get; set; }
        public int ProductionHours { get; set; }
        public float HourCost { get; set; }
        public float AdditionalCost { get; set; }
        public float ProfitPercentage { get; set; }
        public float FinalPrice { get; set; }
    }
}
