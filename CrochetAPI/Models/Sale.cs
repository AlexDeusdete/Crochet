using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Observation { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
