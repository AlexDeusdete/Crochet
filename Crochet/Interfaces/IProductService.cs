﻿using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> GetItems();
        Task<IList<Product>> GetItemsByType(int productTypeid);
        Task<IList<ProductGroup>> GetGroupItems();
        Task<Product> GetItemByName(string Name);
        Task<Product> UpsertItem(Product Item);
    }
}
