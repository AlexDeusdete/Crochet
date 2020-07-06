using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode 
        {
            get
            {
                return Id.ToString().PadLeft(5, '0');
            }
            set { }
        }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Difficulty { get; set; }
        public string Comments { get; set; }
    }
}
