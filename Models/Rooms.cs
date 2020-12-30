using System.ComponentModel.DataAnnotations;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public enum Rooms
    {
        None,
        [Display(Name = "1 rum och kokvrå")]
        OneRoomWithKitchenette,
        [Display(Name = "1 rum och kök")]
        OneRoomWithKitchen,
        [Display(Name = "2 rum och kök")]
        TwoRoomsWithKitchen,
        [Display(Name = "3 rum och kök")]
        ThreeRoomsWithKitchen,
        [Display(Name = "4 rum och kök")]
        FourRoomsWithKitchen,
        [Display(Name = "5 rum och kök")]
        FiveRoomsWithKitchen,
        [Display(Name = "6 rum och kök")]
        SixRoomsWithKitchen       
    }
}