using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LimoncelloShop.Data
{
    public class LimoncelloDbContext : IdentityDbContext<User>
    {
        public DbSet<Limoncello> Limoncello { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
        public DbSet<Basket> Basket { get; set; }

        public LimoncelloDbContext() : base() { }

        public LimoncelloDbContext(DbContextOptions<LimoncelloDbContext> options) : base(options) { }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Basket>()
        //        .HasOne(t => t.User)
        //        .WithMany()
        //        .IsRequired(false)
        //        .OnDelete(DeleteBehavior.NoAction);

        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LimoncelloDb"));
            }

            base.OnConfiguring(optionsBuilder);
        }

    }
}