using System;
using Microsoft.AspNetCore.Identity;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            RegistrationDate = DateTime.Now;
        }
        [PersonalData]
        public DateTime RegistrationDate { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string StreetAddress { get; set; }
    }
}