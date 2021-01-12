using System;
using System.Collections.Generic;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class NewListingEditViewModel
    {
        public string RentalObjectID { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public DateTime MoveInDate { get; set; }
    }
}
