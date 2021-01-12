using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{   
    public class ActiveContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RentalObjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

