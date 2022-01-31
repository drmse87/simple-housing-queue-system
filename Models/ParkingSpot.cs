using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{
    public class ParkingSpot : RentalObject
    {
        public int ParkingSpotNumber { get; set; }
    }
}