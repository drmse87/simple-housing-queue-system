using System;
using System.Collections.Generic;

namespace simple_housing_queue_system.Models
{
    public class AdminViewModel
    {
        public IEnumerable<AdminListingViewModel> AllListingsAndQueingApplicants { get; set; }
        public IEnumerable<ActiveContract> ActiveContracts { get; set; }
        public IEnumerable<RentalObjectsPerAreaCount> RentalObjectsPerAreaCounts { get; set; }
        public IEnumerable<RentalObject> RentalObjectsWithoutContracts { get; set; }
        public IEnumerable<RentalObject> RentalObjectsWithoutContractsAndListings { get; set; }
        public NewContractEditViewModel NewContractEditViewModel { get; set; }
    }
}
