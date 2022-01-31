using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace simple_housing_queue_system.Models
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ContractID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser {get; set; }

        [ForeignKey("RentalObject")]
        public string RentalObjectID { get; set; }
        public RentalObject RentalObject { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ContractAwardedDate { get; set; }
    }
}