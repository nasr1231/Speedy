using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Speedy.Core.Models;
using Speedy.Core.Models.RelatedData;
using Speedy.Seeds;
using System.Drawing;
using System.Reflection.Emit;

namespace Speedy.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        #region Related Data
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ServiceArea> ServiceAreas { get; set; }

        #endregion
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<StartUp> StartUps { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


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
           .HasOne(u => u.City)
           .WithMany()
           .HasForeignKey(u => u.CityId)
           .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Created and Update
            builder.Entity<Delivery>()
               .HasOne(b => b.CreatedBy)
               .WithMany()
               .HasForeignKey(b => b.CreatedById)
               .OnDelete(DeleteBehavior.NoAction);

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



            builder.Entity<Review>()
           .HasOne(u => u.StartUp)
           .WithMany(u => u.Reviews)
           .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
            .Property(r => r.PaymentStatus)
            .HasConversion<string>();

            builder.HasSequence<int>("TrackingNumber").
                StartsAt(100001)
                .IncrementsBy(5);

            builder.Entity<Order>()
                .Property(tr => tr.TrackingNumber).
                HasDefaultValueSql("NEXT VALUE FOR TrackingNumber");


            // Configuring Compsite Primary Key for delivery
            builder.Entity<DeliveryServiceArea>().HasKey(dsa => new { dsa.ServiceAreaId, dsa.DeliveryId });
            #endregion

            #region Seeds
            builder.Entity<ShippingMethod>().HasData(RelevantData.ShippingMethods);
            builder.Entity<Governorate>().HasData(RelevantData.Governorates);
            builder.Entity<City>().HasData(RelevantData.Cities);            
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
