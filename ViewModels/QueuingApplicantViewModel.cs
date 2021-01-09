using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
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

