using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{   
    public class QueuingApplicantViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationDate { get; set; }
        public string ApplicationDate { get; set; }
        public string QueueTime { get; set; }
    }
}

