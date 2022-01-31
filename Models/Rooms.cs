using System.ComponentModel.DataAnnotations;

namespace simple_housing_queue_system.Models
{
    public enum Rooms
    {
        None,
        [Display(Name = "1 room with kitchenette")]
        OneRoomWithKitchenette,
        [Display(Name = "1 room with kitchen")]
        OneRoomWithKitchen,
        [Display(Name = "2 rooms with kitchen")]
        TwoRoomsWithKitchen,
        [Display(Name = "3 rooms with kitchen")]
        ThreeRoomsWithKitchen,
        [Display(Name = "4 rooms with kitchen")]
        FourRoomsWithKitchen,
        [Display(Name = "5 rooms with kitchen")]
        FiveRoomsWithKitchen,
        [Display(Name = "6 rooms with kitchen")]
        SixRoomsWithKitchen       
    }
}