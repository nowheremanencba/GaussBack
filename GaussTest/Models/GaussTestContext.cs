using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaussTest.Models
{
    public class GaussTestContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Marca> Marca { get; set; }

        public GaussTestContext(DbContextOptions<GaussTestContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>().HasData(new Marca { ID = 1, Nombre  = "Adidas" });
            modelBuilder.Entity<Marca>().HasData(new Marca { ID = 2, Nombre = "Topper" });
            modelBuilder.Entity<Marca>().HasData(new Marca { ID = 3, Nombre = "Nike" });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=DBGaussTest;");
        //}
    }
}
