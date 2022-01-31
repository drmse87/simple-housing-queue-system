using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{   
    public class ListingDetailViewModel
    {
        public string ListingID { get; set; }
        public string RentalObjectID { get; set; }
        public string RentalObjectType { get; set; }
        public string Floor { get; set; }
        public string Rooms { get; set; }
        public string Rent { get; set; }
        public string Size { get; set; }
        public string FloorPlanUrl { get; set; }
        public string StreetAddress { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyPhotoUrl { get; set; }
        public string AreaDescription { get; set; }
        public string Name { get; set; }
        public string PublishDate { get; set; }
        public string LastApplicationDate { get; set; }
        public string MoveInDate { get; set; }
    }
}

