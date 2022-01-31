using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace simple_housing_queue_system.Models
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