using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
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

