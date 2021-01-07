using System.ComponentModel.DataAnnotations;
using System;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class ListingEditViewModel
    {
        [Display(Name="Rental object ID")]
        [Required(ErrorMessage = "Please enter valid rental object ID.")]
        public string RentalObjectID { get; set; }
        [Display(Name="Last application date")]
        [Required(ErrorMessage = "Please enter valid last application date.")]
        public DateTime LastApplicationDate { get; set; }
        [Display(Name="Move in date")]
        [Required(ErrorMessage = "Please enter valid move in date.")]
        public DateTime MoveInDate { get; set; }
    }
}