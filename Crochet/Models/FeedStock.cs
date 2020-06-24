using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xamarin.Essentials;

namespace Crochet.Models
{
    public class FeedStock
    {
        public int FeedStockId { get; set; }

        public int[] ColorsArgb { get; set; } 

        public IList<Color> Colors
        {
            get
            {
                return ColorsArgb.Select(x => Color.FromArgb(x)).ToList();                
            }
            set
            {
                ColorsArgb = value.Select(x => x.ToArgb()).ToArray();
            }
        }
        public string ColorCode { get; set; }
        public int Thickness { get; set; }
        public string TEX { get; set; }
        public float Price { get; set; }
        public int InventoryAvailable { get; set; }
        public int InventoryTotal { get; set; }
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
