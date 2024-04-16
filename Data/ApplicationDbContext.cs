using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUsers { get; set; } 
        public DbSet<Delivery> Deliveries { get; set; } 
        public DbSet<ShippingMethod> ShippingMethods { get; set; } 
    }
}
