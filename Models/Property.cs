using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_asp_net_core_mvc_housing_queue.Models
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PropertyID { get; set; }

        [ForeignKey("Area")]
        public string AreaID { get; set; }
        public Area Area { get; set; }

        public string StreetAddress { get; set; }
        public string Description { get; set; }
    }
}