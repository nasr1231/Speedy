using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    { 
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region Setting Relationships
            builder.Entity<AppUser>()
           .HasOne(u => u.Delivery)
           .WithOne(u => u.AppUser)
           .HasForeignKey<Delivery>(u => u.AppUserId)
           .OnDelete(DeleteBehavior.Cascade);                
            
            #endregion
            base.OnModelCreating(builder);
        }                
    }
}
