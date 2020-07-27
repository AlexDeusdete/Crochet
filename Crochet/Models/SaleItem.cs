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
        public int Qtd { get; set; }
        public float Price { get; set; }

        public Product Product { get; set; }

    }
}
