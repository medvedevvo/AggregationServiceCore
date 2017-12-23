using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AggregationService;

namespace AggregationService.Models
{
    /*public class DriversDbContext :  DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DriversDbContext() : base() { }
        public DriversDbContext(DbContextOptions<DriversDbContext> ops) : base(ops) { }
    }*/

    public class DriversDbContext :  DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DriversDbContext() : base() { }
        public DriversDbContext(DbContextOptions<DriversDbContext> ops) : base(ops) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=Drivers.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";//"Server=tcp:medvedev.database.windows.net,1433;Initial Catalog=CarMonitoring;Persist Security Info=False;User ID=medvedev_vo;Password=028545vm!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            builder.UseSqlServer(connection);
            base.OnConfiguring(builder);
        }
    }
}