using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace super_heroi_api.Models
{
    public class Herois
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(120)]
        public string NomeHeroi { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateTime? DataNascimento { get; set; }

        [Required]
        public float Altura { get; set; }

        [Required]
        public float Peso { get; set; }

        public ICollection<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }
    }
}
