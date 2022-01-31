using System;
using System.Collections.Generic;

namespace simple_housing_queue_system.Models
{   
    public class AdminListingViewModel
    {
        public IEnumerable<QueuingApplicantViewModel> QueuingApplicants { get; set; }
        public string RentalObjectID { get; set; }
        public string ListingID { get; set; }
        public string Name { get; set; }
        public string Rooms { get; set; }
        public string Size { get; set; }
        public string Rent { get; set; }
        public string StreetAddress { get; set; }
        public string PropertyPhotoUrl { get; set; }
        public string PublishDate { get; set; }
        public string LastApplicationDate { get; set; }
        public string MoveInDate { get; set; }
    }
}