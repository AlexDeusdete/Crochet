using Crochet.Models;
using Crochet.Utils;
using LiteDB;
using Xamarin.Forms;

namespace Crochet.Services.LiteDB
{
    public class LiteDBBase
    {
        private static LiteDatabase _liteDatabase;
        public LiteDatabase GetDBInstance()
        {
            if (_liteDatabase == null)
            {
                Mapping();
                _liteDatabase = new LiteDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Crochet.db"));
            }
                

            return _liteDatabase;
        }

        private void Mapping()
        {
            var mapper = BsonMapper.Global;
            mapper.Entity<FeedStock>()
                .Ignore(x => x.Colors);

            mapper.Entity<FeedStock>()
                .DbRef(x => x.Brand, "brand");

            mapper.Entity<ProductYarn>()
                .DbRef(x => x.Yarn, "feedstock");

            mapper.Entity<Product>()
                .Ignore(x => x.ProductCode);

            mapper.Entity<ProductPicture>()
                .Ignore(x => x.Uri);

            mapper.Entity<ProductFinalcial>()
                .Ignore(x => x.LaborCost);

            mapper.Entity<ProductFinalcial>()
                .Ignore(x => x.SuggestedPrice);

            mapper.Entity<ProductFinalcial>()
                .Ignore(x => x.ProfitPracticed);

            mapper.Entity<ProductFinalcial>()
                .Ignore(x => x.ProfitValue);
        }
    }
}
