using System;

namespace super_heroi_api.Models.DTO
{
    public class CadastrarHeroi
    {
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }
        public DateTime? DataNascimento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }

        // Lista de IDs dos superpoderes
        public List<int> SuperpoderesIds { get; set; }
    }
}
