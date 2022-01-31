using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{
    public class RentalObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RentalObjectID { get; set; }

        [ForeignKey("Property")]
        public string PropertyID { get; set; }
        public Property Property { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Rent { get; set; }
    }
}