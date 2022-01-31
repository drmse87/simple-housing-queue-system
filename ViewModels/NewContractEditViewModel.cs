using System;
using System.Collections.Generic;

namespace simple_housing_queue_system.Models
{
    public class NewContractEditViewModel
    {
        public string RentalObjectID { get; set; }
        public string ListingID { get; set; }
        public string UserID { get; set; }
        public DateTime StartDate { get; set; }
    }
}
