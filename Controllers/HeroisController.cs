using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using super_heroi_api.Data;
using super_heroi_api.Models;
using super_heroi_api.Models.DTO;

namespace super_heroi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HeroisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Herois
        [HttpGet]
        public async Task<IActionResult> ObterTodosHerois()
        {
            var herois = await _context.Herois.ToListAsync();

            if (herois == null || !herois.Any())
            {
                return NotFound("Não há super-heróis cadastrados.");
            }

            return Ok(herois);
        }

        //GET por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterHeroiPorId(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);

            if (heroi == null)
            {
                return NotFound($"Super-herói com Id {id} não encontrado.");
            }

            return Ok(heroi);
        }

        // POST: api/Herois
        [HttpPost]
        public async Task<IActionResult> CriarHeroi([FromBody] CadastrarHeroi cadastrarHeroi)
        {
            if (cadastrarHeroi == null || !cadastrarHeroi.SuperpoderesIds.Any())
            {
                return BadRequest("Todos os campos são obrigatórios, incluindo a lista de superpoderes.");
            }

            var heroiExistente = await _context.Herois
                .FirstOrDefaultAsync(h => h.NomeHeroi.Equals(cadastrarHeroi.NomeHeroi, StringComparison.OrdinalIgnoreCase));

            if (heroiExistente != null)
            {
                return Conflict($"Já existe um super-herói com o nome de herói '{cadastrarHeroi.NomeHeroi}'.");
            }

            var heroi = new Herois
            {
                Nome = cadastrarHeroi.Nome,
                NomeHeroi = cadastrarHeroi.NomeHeroi,
                DataNascimento = cadastrarHeroi.DataNascimento,
                Altura = cadastrarHeroi.Altura,
                Peso = cadastrarHeroi.Peso
            };

            _context.Herois.Add(heroi);
            await _context.SaveChangesAsync();

            var superpoderes = await _context.Superpoderes
                .Where(sp => cadastrarHeroi.SuperpoderesIds.Contains(sp.Id))
                .ToListAsync();

            if (superpoderes.Count != cadastrarHeroi.SuperpoderesIds.Count)
            {
                return BadRequest("Alguns superpoderes não foram encontrados.");
            }

            foreach (var superpoder in superpoderes)
            {
                _context.HeroisSuperpoderes.Add(new HeroisSuperpoderes
                {
                    HeroiId = heroi.Id,
                    SuperpoderId = superpoder.Id
                });
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CriarHeroi), new { id = heroi.Id }, heroi);
        }


        // PUT: api/Herois/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, [FromBody] CadastrarHeroi heroiAtualizado)
        {
            // Verifica se o herói com o Id informado existe no banco de dados
            var heroiExistente = await _context.Herois.FindAsync(id);

            if (heroiExistente == null)
            {
                // Caso o herói não seja encontrado, retorna 404 Not Found
                return NotFound($"Super-herói com Id {id} não encontrado.");
            }

            // Verifica se já existe outro herói com o mesmo nome de herói, excluindo o herói atual
            var nomeHeroiJaExiste = await _context.Herois
                .AnyAsync(h => h.NomeHeroi == heroiAtualizado.NomeHeroi && h.Id != id);

            if (nomeHeroiJaExiste)
            {
                // Se já existir outro herói com o mesmo nome, retorna 400 Bad Request
                return BadRequest($"O nome de herói '{heroiAtualizado.NomeHeroi}' já está em uso por outro super-herói.");
            }

            // Atualiza as propriedades do herói com os novos dados
            heroiExistente.Nome = heroiAtualizado.Nome;
            heroiExistente.NomeHeroi = heroiAtualizado.NomeHeroi;
            heroiExistente.DataNascimento = heroiAtualizado.DataNascimento;
            heroiExistente.Altura = heroiAtualizado.Altura;
            heroiExistente.Peso = heroiAtualizado.Peso;

            // Remove os superpoderes antigos e associa os novos
            var superpoderesExistentes = await _context.HeroisSuperpoderes
                .Where(hs => hs.HeroiId == heroiExistente.Id)
                .ToListAsync();

            _context.HeroisSuperpoderes.RemoveRange(superpoderesExistentes);

            var superpoderes = await _context.Superpoderes
                .Where(sp => heroiAtualizado.SuperpoderesIds.Contains(sp.Id))
                .ToListAsync();

            foreach (var superpoder in superpoderes)
            {
                _context.HeroisSuperpoderes.Add(new HeroisSuperpoderes
                {
                    HeroiId = heroiExistente.Id,
                    SuperpoderId = superpoder.Id
                });
            }

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna a resposta HTTP 200 (OK) com o herói atualizado
            return Ok(heroiExistente);
        }

        // DELETE: api/Herois/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirHeroi(int id)
        {
            // Verifica se o super-herói com o Id informado existe no banco de dados
            var heroi = await _context.Herois.FindAsync(id);

            if (heroi == null)
            {
                // Caso o herói não seja encontrado, retorna 404 Not Found
                return NotFound($"Super-herói com Id {id} não encontrado.");
            }

            // Remove o super-herói da tabela
            _context.Herois.Remove(heroi);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna um status 204 (No Content) indicando que a exclusão foi bem-sucedida
            return NoContent();  // 204 No Content, pois não há conteúdo a ser retornado após a exclusão
        }
    }
}
