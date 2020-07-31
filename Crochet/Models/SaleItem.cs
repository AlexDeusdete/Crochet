using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public string VariationName { get; set; }
        public int Qtd { get; set; }
        public float Price { get; set; }
        public float TotalPrice 
        {
            get { return Qtd * Price; }
            set { } 
        }

        public Product Product { get; set; }

    }
}
