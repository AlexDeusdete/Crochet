using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Interfaces
{
    public interface IFeedStockService
    {
        IList<FeedStock> GetItems();
        FeedStock PutItem(FeedStock Item);
    }
}
