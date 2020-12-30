using System.ComponentModel.DataAnnotations;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public enum Floor
    {
        None,
        [Display(Name = "BV")]
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