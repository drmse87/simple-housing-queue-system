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
        public DbSet<Models.ActiveContract> ActiveContracts { get; set; }
        public DbSet<Models.QueuingApplicant> QueuingApplicants { get; set; }
        public DbSet<Models.RentalObjectsPerAreaCount> RentalObjectsPerAreaCounts { get; set; }

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
                });    

            modelBuilder
                .Entity<Models.ListingDetail>(eb => 
                {
                    eb.HasNoKey();
                });

            modelBuilder
                .Entity<Models.ActiveContract>(eb => 
                {
                    eb.HasNoKey();
                });

            modelBuilder
                .Entity<Models.QueuingApplicant>(eb => 
                {
                    eb.HasNoKey();
                });

            modelBuilder
                .Entity<Models.RentalObjectsPerAreaCount>(eb => 
                {
                    eb.HasNoKey();
                });
        }
    }
}
