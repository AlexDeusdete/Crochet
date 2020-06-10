using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crochet.Services.LiteDB
{
    public class FeedStockService : IFeedStockService
    {
        private LiteDatabase _dataBase;

        public FeedStockService()
        {
            _dataBase = new LiteDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Crochet.db"));
        }
        public async Task<IList<FeedStockGroup>> GetGroupItems()
        {
            var feedStockGroups = new List<FeedStockGroup>();

            var feedStockItems = await GetItems();

            foreach (var feedStocks in feedStockItems
                                        .GroupBy(x => x.Color.GetHue())
                                        .Select(grp => grp.ToList())
                                        .ToList())
            {                
                var feedStockGroup = new FeedStockGroup(feedStocks[0].Color.GetHue().ToString());

                var feedStockCollection = new FeedStockCollection();
                feedStockCollection.AddRange(feedStocks);
                feedStockGroup.Add(feedStockCollection);                
            }

            return feedStockGroups;
        }

        public async Task<IList<FeedStock>> GetItems()
        {
            ILiteCollection<FeedStock> feedStocks = _dataBase.GetCollection<FeedStock>();

            return await Task.FromResult(feedStocks.FindAll().ToList());
        }

        public void UpsertItem(FeedStock Item)
        {
            ILiteCollection<FeedStock> feedStocks = _dataBase.GetCollection<FeedStock>();

            feedStocks.Upsert(Item);
        }
    }
}
