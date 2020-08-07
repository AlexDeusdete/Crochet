using DryIoc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Crochet.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId 
        {
            get { return Customer.Id; }
            set { } 
        }
        public DateTime SaleDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Observation { get; set; }
        public float TotalPrice { get; set; }
        public float Discount { get; set; }
        public int Status { get; set; }
        public bool Finalized { get; set; }
        [JsonIgnore]
        public float TotalPriceAfterDiscount 
        {
            get { return TotalPrice - Discount; }
            set { }
        }

        public Customer Customer { get; set; }
    }
}
