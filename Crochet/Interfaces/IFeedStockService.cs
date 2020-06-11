﻿using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface IFeedStockService
    {
        Task<IList<FeedStock>> GetItems();
        Task<IList<FeedStockGroup>> GetGroupItems();
        void UpsertItem(FeedStock Item);
    }
}