using System.ComponentModel.DataAnnotations;

namespace simple_housing_queue_system.Models
{
    public enum Floor
    {
        None,
        [Display(Name = "Ground floor")]
        GroundFloor,
        [Display(Name = "1")]
        FirstFloor,
        [Display(Name = "2")]
        SecondFloor,
        [Display(Name = "3")]
        ThirdFloor,
        [Display(Name = "4")]
        FourthFloor,
        [Display(Name = "5")]
        FifthFloor,
        [Display(Name = "6")]
        SixthFloor,
        [Display(Name = "7")]
        SeventhFloor,
        [Display(Name = "8")]
        EigthFloor,
        [Display(Name = "9")]
        NinthFloor,
        [Display(Name = "10")]
        TenthFloor
    }
}