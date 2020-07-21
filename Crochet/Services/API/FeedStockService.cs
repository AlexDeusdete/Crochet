using Crochet.Extensions;
using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class FeedStockService : ApiBase, IFeedStockService
    {
        public FeedStockService(IApi api) : base(api){}
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
            return await API.GetYarns();
        }

        public async Task<FeedStock> UpsertItem(FeedStock Item)
        {
            if (Item.FeedStockId > 0)
                return await API.PutYarn(Item.FeedStockId, Item);
            else
                return await API.PostYarn(Item);
        }
    }
}
