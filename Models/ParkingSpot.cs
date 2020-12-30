using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class ParkingSpot : RentalObject
    {
        public int ParkingSpotNumber { get; set; }
    }
}