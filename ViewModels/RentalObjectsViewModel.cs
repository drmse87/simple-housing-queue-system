using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class RentalObjectsViewModel
    {
        public IEnumerable<RentalObject> RentalObjects { get; set; }
    }
}