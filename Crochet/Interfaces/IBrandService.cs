using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IBrandService
    {
        Task<IList<Brand>> GetItems();
        Task<Brand> PutItem(Brand Item);
    }
}
