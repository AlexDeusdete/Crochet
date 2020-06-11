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
            mapper.Entity<FeedStock>().Ignore(x => x.Color);
        }
    }
}
