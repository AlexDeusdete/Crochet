using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Interfaces
{
    public interface IBrand
    {
        IList<Brand> GetItems();
        Brand PutItem(Brand Item);
    }
}
