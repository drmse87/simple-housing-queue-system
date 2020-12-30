using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class Apartment : RentalObject
    {
        public string FloorPlanUrl { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Size { get; set; }
        public Rooms Rooms { get; set; }
        public Floor Floor { get; set; }
    }
}