using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace simple_housing_queue_system.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ApplicationID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Listing")]
        public string ListingID { get; set; }
        public Listing Listing { get; set; }

        public DateTime ApplicationDate { get; set; }
    }
}