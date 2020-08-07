using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface ISaleService
    {
        Task<IList<Sale>> GetSales(int? status, bool? finalized = null);
        Task<Sale> GetSaleById(int id);
        Task<Sale> PutInsertSale(Sale sale);
        Task<IList<SaleItem>> GetSaleItems(int? sale);
        Task<SaleItem> GetSaleItemById(int id);
        Task<SaleItem> PutInsertSaleItem(SaleItem sale);
    }
}
