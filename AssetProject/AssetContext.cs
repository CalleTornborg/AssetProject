using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    public class AssetContext :DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Server = (localdb)\\MSSQLLocalDB; Database = EFAssets;");
        }
    }
}
