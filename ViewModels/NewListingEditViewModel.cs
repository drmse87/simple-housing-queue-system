using System;
using System.Collections.Generic;

namespace simple_housing_queue_system.Models
{
    public class NewListingEditViewModel
    {
        public string RentalObjectID { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public DateTime MoveInDate { get; set; }
    }
}
