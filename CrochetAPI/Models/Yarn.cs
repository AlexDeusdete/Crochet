using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Crochet.Models
{
    public class Yarn
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string InternalColorsArgb { get; set; }
        [NotMapped]
        public int[] ColorsArgb
        {
            get
            {
                if(InternalColorsArgb != null)
                    return Array.ConvertAll(InternalColorsArgb.Split(';'), int.Parse);
                return null;
            }
            set
            {
                if(value != null)
                    InternalColorsArgb = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        public string ColorCode { get; set; }
        public string ColorName { get; set; }
        public int Thickness { get; set; }
        public string TEX { get; set; }
        public float Price { get; set; }
        public int InventoryAvailable { get; set; }
        public int InventoryTotal { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
