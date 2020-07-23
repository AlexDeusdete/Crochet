using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IProductYarnService
    {
        Task<IList<ProductYarn>> GetYarns(int productId);

        Task<IList<ProductYarnGroup>> GetProductYarnsGroup(int productId);

        Task<ProductYarn> UpsertItem(ProductYarn productYarn);
        Task<ProductYarn> DeleteItem(ProductYarn productYarn);
    }
}
