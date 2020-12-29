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
        public DbSet<Models.Property> Properties { get; set; }
        public DbSet<Models.Area> Areas { get; set; }
        public DbSet<Models.Listing> Listings { get; set; }
        public DbSet<Models.Application> Applications { get; set; }
        public DbSet<Models.Contract> Contracts { get; set; }
    }
}
