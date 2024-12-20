using System.ComponentModel.DataAnnotations;

namespace super_heroi_api.Models
{
    public class Superpoderes
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Superpoder { get; set; }

        [MaxLength(250)]
        public string? Descricao { get; set; }
    }
}
