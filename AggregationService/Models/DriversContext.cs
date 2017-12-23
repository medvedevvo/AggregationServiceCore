using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AggregationService;

namespace AggregationService.Models
{
    public class DriversDbContext :  DbContext
    {
        private ConnectionSettings connectionSettings = ConnectionSettings.getInstance();

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DriversDbContext() : base() { }
        public DriversDbContext(DbContextOptions<DriversDbContext> ops) : base(ops) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(connectionSettings.ConnecnionString);
            base.OnConfiguring(builder);
        }
    }
}