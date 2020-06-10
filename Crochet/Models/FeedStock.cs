using System;
using System.Collections.Generic;
using System.Drawing;

namespace Crochet.Models
{
    public class FeedStock
    {
        public int FeedStockId { get; set; }

        public int ColorArgb { 
            get
            {
                return Color.ToArgb();
            } 
            set
            {
                Color = Color.FromArgb(ColorArgb);
            } }
        public Color Color { get; set; }
        public int Thickness { get; set; }
        public string TEX { get; set; }
        public float Price { get; set; }
        public int Inventory { get; set; }
        public Brand Brand { get; set; }
    }

    public class FeedStockCollection
    {
        public List<FeedStock> FeedStocks { get;private set; }

        public FeedStockCollection()
        {
            FeedStocks = new List<FeedStock>();
        }

        public void Add(FeedStock feedStock)
        {
            FeedStocks.Add(feedStock);
        }

        public void AddRange(IEnumerable<FeedStock> feedStock)
        {
            FeedStocks.AddRange(feedStock);
        }
    }

    public class FeedStockGroup : List<FeedStockCollection>
    {
        public string Hue { get; set; }

        public FeedStockGroup(string hue)
        {
            Hue = hue;
        }
    }
}
