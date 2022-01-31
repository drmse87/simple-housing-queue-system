using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{   
    public class OpenListing
    {
        public string RentalObjectID { get; set; }
        public string ListingID { get; set; }
        public string Name { get; set; }
        public Rooms Rooms { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Size { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rent { get; set; }
        public string StreetAddress { get; set; }
        public string PropertyPhotoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public DateTime MoveInDate { get; set; }
    }
}