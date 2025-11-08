using Congress_Backend.Data;
using Congress_Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Congress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListadoController : ControllerBase
    {
        private readonly CongressDbContext _context;
        private readonly ILogger<ListadoController> _logger;

        public ListadoController(CongressDbContext context, ILogger<ListadoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// Obtener todos los participantes registrados
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ListParticipantesDto>>> GetAllParticipants()
        {
            try
            {
                var participants = await _context.Participantes
                    .Select(p => new ListParticipantesDto
                    {
                        Id = p.Id,
                        FullName = $"{p.Name} {p.LastName}",
                        TwitterUser = p.TwitterUser,
                        Occupation = p.Occupation,
                        AvatarURL = p.AvatarURL
                    })
                    .ToListAsync();

                return Ok(participants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar todos los participantes");
                return StatusCode(500, new { message = "Ocurrió un error al recuperar los participantes." });
            }
        }

        /// Buscar participantes por nombre
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListParticipantesDto>>> SearchParticipants([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest(new { message = "El parámetro de búsqueda 'name' es requerido." });
                }

                var searchTerm = name.Trim().ToLower();

                var participants = await _context.Participantes
                    .Where(p => p.Name.ToLower().Contains(searchTerm))
                    .Select(p => new ListParticipantesDto
                    {
                        Id = p.Id,
                        FullName = $"{p.Name} {p.LastName}",
                        TwitterUser = p.TwitterUser,
                        Occupation = p.Occupation,
                        AvatarURL = p.AvatarURL
                    })
                    .ToListAsync();

                return Ok(participants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar participantes con término: {Query}", name);
                return StatusCode(500, new { message = "Ocurrió un error al buscar los participantes." });
            }
        }
    }
}
