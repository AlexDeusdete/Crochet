using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class ProductYarn
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Cost { get; set; }
        public float Consumption { get; set; }
        public FeedStock Yarn { get; set; }
        public int VariationId { get; set; }
        public string VariationName { get; set; }
    }

    public class ProductYarnCollection
    {
        public List<ProductYarn> ProductYarns { get;private set; }
        public int VariationId { get; private set; }
        public string VariationName { get; private set; }

        public ProductYarnCollection(int variationId, string variationName)
        {
            ProductYarns = new List<ProductYarn>();
            VariationId = variationId;
            VariationName = variationName;
        }

        public void Add(ProductYarn productYarn)
        {
            ProductYarns.Add(productYarn);
        }

        public void AddRange(IEnumerable<ProductYarn> productYarns)
        {
            ProductYarns.AddRange(productYarns);
        }
    }

    public class ProductYarnGroup : List<ProductYarnCollection>
    {
        public int VariationId { get;private set; }
        public string VariationName { get;private set; }
        public float TotalCost
        {
            get
            {
                float result = 0;

                foreach (var item in this[0].ProductYarns)
                {
                    result += item.Cost;
                }

                return result;
            }
            set { }
        }

        public float TotalConsumption
        {
            get
            {
                float result = 0;
                foreach (var item in this[0].ProductYarns)
                {
                    result += item.Consumption;
                }

                return result;
            }
            set
            { }
        }

        public ProductYarnGroup(int variationId, string variationName)
        {
            VariationId = variationId;
            VariationName = variationName;
        }
    }
}
