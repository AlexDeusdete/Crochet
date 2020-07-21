using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrochetAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(CrochetAPIContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
