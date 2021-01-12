using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class Listing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ListingID { get; set; }

        [ForeignKey("RentalObject")]
        public string RentalObjectID { get; set; }
        public RentalObject RentalObject { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public DateTime MoveInDate { get; set; }
        public DateTime? ListingClosureDate { get; set; }
    }
}