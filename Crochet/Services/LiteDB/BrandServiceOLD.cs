using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Utils;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crochet.Services.LiteDB
{
    public class BrandServiceOLD : LiteDBBase, IBrandService
    {
        private readonly ILiteCollection<Brand> _liteCollection;

        public BrandServiceOLD()
        {
            var dataBase = GetDBInstance();
            _liteCollection = dataBase.GetCollection<Brand>();
        }
        public async Task<IList<Brand>> GetItems()
        {
            return await Task.FromResult(_liteCollection.FindAll().ToList());
        }

        Task<Brand> IBrandService.PutItem(Brand Item)
        {
            _liteCollection.Upsert(Item);
            return null;
        }
    }
}
