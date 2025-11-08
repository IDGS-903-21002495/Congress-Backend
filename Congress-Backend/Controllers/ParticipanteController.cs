using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Congress_Backend.Data;
using Congress_Backend.Models;
using Congress_Backend.DTOs;

namespace Congress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipanteController : ControllerBase
    {
        private readonly CongressDbContext _context;
        private readonly ILogger<ParticipanteController> _logger;

        public ParticipanteController(CongressDbContext context, ILogger<ParticipanteController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// Obtener un participante por ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailParticipanteDto>> GetParticipantById(int id)
        {
            try
            {
                var participant = await _context.Participantes.FindAsync(id);

                if (participant == null)
                {
                    return NotFound(new { message = $"Participante con ID {id} no encontrado." });
                }

                var detailDto = new DetailParticipanteDto
                {
                    Id = participant.Id,
                    Name = participant.Name,
                    LastName = participant.LastName,
                    Email = participant.Email,
                    TwitterUser = participant.TwitterUser,
                    Occupation = participant.Occupation,
                    AvatarURL = participant.AvatarURL
                };

                return Ok(detailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar el participante con ID: {Id}", id);
                return StatusCode(500, new { message = "Ocurrió un error al recuperar el participante." });
            }
        }

    }
}
