using Crochet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrochetAPI.Data
{
    public class CrochetAPIContext : DbContext
    {
        public CrochetAPIContext(DbContextOptions<CrochetAPIContext> options) : base(options)
        {}

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFinalcial> ProductFinalcials { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductYarn> ProductYarns { get; set; }
        public DbSet<Yarn> Yarns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(b => b.Name)
                .IsUnique();
        }
    }
}
