using System;
using System.Collections.Generic;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class NewContractEditViewModel
    {
        public string RentalObjectID { get; set; }
        public string ListingID { get; set; }
        public string UserID { get; set; }
        public DateTime StartDate { get; set; }
    }
}
