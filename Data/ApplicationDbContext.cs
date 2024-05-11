using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Core.Models.RelatedData;
using Speedy.Seeds;
using System.Drawing;

namespace Speedy.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        #region Related Data
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        #endregion

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<StartUp> StartUps { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region Setting Relationships
            builder.Entity<AppUser>()
           .HasOne(u => u.Delivery)
           .WithOne(u => u.AppUser)
           .HasForeignKey<Delivery>(u => u.AppUserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
           .HasOne(u => u.Individual)
           .WithOne(u => u.AppUser)
           .HasForeignKey<Individual>(u => u.AppUserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
           .HasOne(u => u.Startup)
           .WithOne(u => u.AppUser)
           .HasForeignKey<StartUp>(u => u.AppUserId)
           .OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Delivery>()
			   .HasOne(b => b.CreatedBy)
			   .WithMany()
			   .HasForeignKey(b => b.CreatedById)
			   .OnDelete(DeleteBehavior.NoAction);
			#endregion

			#region Created and Update
			builder.Entity<Delivery>()
				.HasOne(b => b.LastUpdatedBy)
				.WithMany()
				.HasForeignKey(b => b.LastUpdatedById)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Individual>()
			   .HasOne(b => b.CreatedBy)
			   .WithMany()
			   .HasForeignKey(b => b.CreatedById)
			   .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Individual>()
				.HasOne(b => b.LastUpdatedBy)
				.WithMany()
				.HasForeignKey(b => b.LastUpdatedById)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<StartUp>()
			   .HasOne(b => b.CreatedBy)
			   .WithMany()
			   .HasForeignKey(b => b.CreatedById)
			   .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<StartUp>()
				.HasOne(b => b.LastUpdatedBy)
				.WithMany()
				.HasForeignKey(b => b.LastUpdatedById)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion

			#region Seeds
			builder.Entity<ShippingMethod>().HasData(RelevantData.ShippingMethods);
            builder.Entity<Governorate>().HasData(RelevantData.Governorates);
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
