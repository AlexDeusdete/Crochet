using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class SaleService : ApiBase, ISaleService
    {
        public SaleService(IApi api):base(api){}
        public async Task<Sale> GetSaleById(int id)
        {
            return await API.GetSale(id);
        }

        public async Task<SaleItem> GetSaleItemById(int id)
        {
            return await API.GetSaleItem(id);
        }

        public async Task<IList<SaleItem>> GetSaleItems(int? sale)
        {
            return await API.GetSaleItems(sale);
        }

        public async Task<IList<Sale>> GetSales(int? status, bool? finalized = null)
        {
            return await API.GetSales(status, finalized);
        }

        public async Task<Sale> PutInsertSale(Sale sale)
        {
            if(sale.Id == 0)
                return await API.PostSale(sale);
            else
            {
                await API.PutSale(sale.Id, sale);
                return sale;
            }                
        }

        public async Task<SaleItem> PutInsertSaleItem(SaleItem sale)
        {
            if (sale.Id == 0)
                return await API.PostSaleItems(sale);
            else
            {
                await API.PutSaleItem(sale.Id, sale);
                return sale;
            }
        }
    }
}
