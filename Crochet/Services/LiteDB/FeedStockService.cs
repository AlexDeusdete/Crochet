﻿using Crochet.Extensions;
using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Utils;
using DryIoc;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Crochet.Services.LiteDB
{
    public class FeedStockService : LiteDBBase, IFeedStockService
    {
        private readonly ILiteCollection<FeedStock> _liteCollection;

        public FeedStockService()
        {
            var dataBase = GetDBInstance();
            _liteCollection = dataBase.GetCollection<FeedStock>();
        }
        public async Task<IList<FeedStockGroup>> GetGroupItems()
        {
            var feedStockGroups = new List<FeedStockGroup>();

            var feedStockItems = await GetItems();

            foreach (var feedStocks in feedStockItems
                                        .GroupBy(x => x.Colors[0].GetHueName())
                                        .Select(grp => grp.ToList())
                                        .ToList())
            {                
                var feedStockGroup = new FeedStockGroup(feedStocks[0].Colors[0].GetHueName());

                var feedStockCollection = new FeedStockCollection();
                feedStockCollection.AddRange(feedStocks);
                feedStockGroup.Add(feedStockCollection);
                feedStockGroups.Add(feedStockGroup);                
            }

            return feedStockGroups;
        }

        public async Task<IList<FeedStock>> GetItems()
        {
            return await Task.FromResult(_liteCollection.Include(x => x.Brand).FindAll().ToList());
        }

        public void UpsertItem(FeedStock Item)
        {
            _liteCollection.Upsert(Item);
        }
    }
}
