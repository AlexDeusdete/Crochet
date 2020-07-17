using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class ProductFinalcial
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public float YarnsCost { get; set; }
        public int ProductionHours { get; set; }
        public float HourCost { get; set; }
        public float AdditionalCost { get; set; }
        public float ProfitPercentage { get; set; }
        public float FinalPrice { get; set; }
        public float LaborCost
        {
            get
            {
                return ProductionHours * HourCost;
            }
            set { }
        }
        public float SuggestedPrice
        {
            get
            {
                return (YarnsCost + LaborCost + AdditionalCost) * ((ProfitPercentage / 100f) + 1f);
            }
            set { }
        }

        public float ProfitPracticed
        {
            get
            {
                var custo = (YarnsCost + LaborCost + AdditionalCost);
                var lucro = FinalPrice - custo;
                if (FinalPrice > 0)
                    return (100f / custo) * lucro;
                else
                    return 0;
            }
            set { }
        }
        public float ProfitValue
        {
            get
            {
                return FinalPrice * (ProfitPracticed / 100f);
            }
            set { }
        }
    }
}
