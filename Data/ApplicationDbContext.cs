using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp_asp_net_core_mvc_housing_queue.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.RentalObject> RentalObjects { get; set; }
        public DbSet<Models.Apartment> Apartments { get; set; }
        public DbSet<Models.ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Models.Property> Properties { get; set; }
        public DbSet<Models.Area> Areas { get; set; }
        public DbSet<Models.Listing> Listings { get; set; }
        public DbSet<Models.Application> Applications { get; set; }
        public DbSet<Models.Contract> Contracts { get; set; }
        public DbSet<Models.OpenListing> OpenListings { get; set; }
        public DbSet<Models.ListingDetail> ListingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Models.RentalObject>()
                    .HasDiscriminator<string>("RentalObjectType")
                    .HasValue<Models.Apartment>("Apartment")
                    .HasValue<Models.ParkingSpot>("Parking spot");

            modelBuilder
                .Entity<Models.OpenListing>(eb => 
                {
                    eb.HasNoKey();
                    eb.ToView("View_AllOpenListings");
                    eb.Property(v => v.ListingID).HasColumnName("ListingID");
                    eb.Property(v => v.Name).HasColumnName("Name");
                    eb.Property(v => v.Rooms).HasColumnName("Rooms");
                    eb.Property(v => v.Size).HasColumnName("Size");
                    eb.Property(v => v.Rent).HasColumnName("Rent");
                    eb.Property(v => v.StreetAddress).HasColumnName("StreetAddress");
                    eb.Property(v => v.PropertyPhotoUrl).HasColumnName("PropertyPhotoUrl");
                    eb.Property(v => v.PublishDate).HasColumnName("PublishDate");
                    eb.Property(v => v.LastApplicationDate).HasColumnName("LastApplicationDate");
                    eb.Property(v => v.MoveInDate).HasColumnName("MoveInDate");
                });    

            modelBuilder
                .Entity<Models.ListingDetail>(eb => 
                {
                    eb.HasNoKey();
                });
        }
    }
}
