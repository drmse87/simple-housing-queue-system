using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{   
    public class MadeApplicationViewModel
    {
        public string ListingID { get; set; }
        public string RentalObjectID { get; set; }
        public string Rooms { get; set; }
        public string Size { get; set; }
        public string Rent { get; set; }
        public string StreetAddress { get; set; }
        public string ApplicationDate { get; set; }
        public string LastApplicationDate { get; set; }
        public string MoveInDate { get; set; }
        public string PlaceInQueue { get; set; }
        public string TotalApplicantsInQueue { get; set; }
    }
}

