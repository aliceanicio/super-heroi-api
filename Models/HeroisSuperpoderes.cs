using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace super_heroi_api.Models
{
    public class HeroisSuperpoderes
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int HeroiId { get; set; }

        [ForeignKey("HeroiId")]
        public Herois Herois { get; set; }

        [Required]
        public int SuperpoderId { get; set; }

        [ForeignKey("SuperpoderId")]
        public Superpoderes Superpoder { get; set; }
    }
}
