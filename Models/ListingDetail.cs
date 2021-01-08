using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{   
    public class ListingDetail
    {
        public string ListingID { get; set; }
        public string RentalObjectID { get; set; }
        public string RentalObjectType { get; set; }
        public Floor Floor { get; set; }
        public Rooms Rooms { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Rent { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Size { get; set; }
        public string FloorPlanUrl { get; set; }
        public string StreetAddress { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyPhotoUrl { get; set; }
        public string AreaDescription { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public DateTime MoveInDate { get; set; }
    }
}

