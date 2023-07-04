
using Microsoft.EntityFrameworkCore;
using Smartwyre.Data.Enum;
using Smartwyre.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
namespace Smartwyre.Data.Context
{
    public class SmartwyreContext : DbContext
    {
        public SmartwyreContext(DbContextOptions<SmartwyreContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("SmartwyreDb");
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rebate> Rebates { get; set; }
        public virtual DbSet<RebateCalculation> RebateCalculations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Rebate>().ToTable("Rebate");
            modelBuilder.Entity<RebateCalculation>().ToTable("RebateCalculation");
        }
    }
}
