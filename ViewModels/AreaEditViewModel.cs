using System.ComponentModel.DataAnnotations;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class AreaEditViewModel
    {
        [Display(Name="Area name")]
        [Required(ErrorMessage = "Please enter area name.")]
        public string Name { get; set; }
        [Display(Name="Area description")]
        [Required(ErrorMessage = "Please enter area description.")]
        public string Description { get; set; }
    }
}