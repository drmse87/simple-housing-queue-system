using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_housing_queue_system.Models
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PropertyID { get; set; }

        [ForeignKey("Area")]
        public string AreaID { get; set; }
        public Area Area { get; set; }
        public string PropertyPhotoUrl { get; set; }
        public string StreetAddress { get; set; }
        public string Description { get; set; }
    }
}