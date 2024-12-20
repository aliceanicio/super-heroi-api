using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using super_heroi_api.Data;
using super_heroi_api.Models;

namespace super_heroi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperpoderesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuperpoderesController(AppDbContext context)
        {
            _context = context;
        }

        // Método para listar todos os superpoderes cadastrados
        [HttpGet]
        public async Task<IActionResult> GetSuperpoderes()
        {
            // Recupera todos os superpoderes cadastrados no banco
            var superpoderes = await _context.Superpoderes.ToListAsync();

            // Verifica se há superpoderes cadastrados
            if (superpoderes == null || !superpoderes.Any())
            {
                return NotFound("Nenhum superpoder encontrado.");
            }

            // Retorna a lista de superpoderes com o código de status 200 (OK)
            return Ok(superpoderes);
        }
    }
}
