using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IProductTypeService
    {
        Task<IList<ProductType>> GetItems();
        Task<ProductType> InsertItem(ProductType Item);
    }
}
