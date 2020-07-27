using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int? ProductTypeId { get { return ProductType == null ? 0 : ProductType.Id; } set { } }
        public string ProductCode 
        {
            get
            {
                return Id.ToString().PadLeft(5, '0');
            }
            set { }
        }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Difficulty { get; set; }
        public string Comments { get; set; }
        public ProductType ProductType { get; set; }
        public IList<ProductPicture> ProductPictures { get; set; }
    }

    public class ProductCollection
    {
        public List<Product> Products { get; private set; }

        public ProductCollection()
        {
            Products = new List<Product>();
        }

        public void Add(Product product)
        {
            Products.Add(product);
        }

        public void AddRange(IEnumerable<Product> products)
        {
            Products.AddRange(products);
        }
    }

    public class ProductGroup : List<ProductCollection>
    {
        public string Grupo { get; set; }
        public ProductGroup(string grupo)
        {
            if (string.IsNullOrEmpty(grupo))
                Grupo = "Sem Grupo";
            else
                Grupo = grupo;
        }
    }
}
