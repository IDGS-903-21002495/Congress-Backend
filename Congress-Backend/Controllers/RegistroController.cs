using Congress_Backend.Data;
using Congress_Backend.DTOs;
using Congress_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Congress_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroController : ControllerBase
    {
        private readonly CongressDbContext _context;
        private readonly ILogger<RegistroController> _logger;
        
        public RegistroController(CongressDbContext context, ILogger<RegistroController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// Registrar un nuevo participante
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetailParticipanteDto>> RegisterParticipant([FromBody] CreateParticipanteDto participantDto)
        {
            try
            {
                // Validar que no exista un participante con el mismo email
                var existingParticipant = await _context.Participantes
                    .FirstOrDefaultAsync(p => p.Email == participantDto.Email);

                if (existingParticipant != null)
                {
                    return BadRequest(new { message = "Ya existe un participante con este email." });
                }

                // Crear un nuevo participante 
                var newParticipant = new Participante
                {
                    Name = participantDto.Name,
                    LastName = participantDto.LastName,
                    Email = participantDto.Email,
                    TwitterUser = participantDto.TwitterUser,
                    Occupation = participantDto.Occupation,
                    AvatarURL = participantDto.AvatarURL
                };

                _context.Participantes.Add(newParticipant);
                await _context.SaveChangesAsync();

                // Mapear a DTO de respuesta
                var responseDto = new DetailParticipanteDto
                {
                    Id = newParticipant.Id,
                    Name = newParticipant.Name,
                    LastName = newParticipant.LastName,
                    Email = newParticipant.Email,
                    TwitterUser = newParticipant.TwitterUser,
                    Occupation = newParticipant.Occupation,
                    AvatarURL = newParticipant.AvatarURL
                };

                _logger.LogInformation("Participante registrado exitosamente con ID: {Id}", newParticipant.Id);

                return CreatedAtRoute(new { id = newParticipant.Id }, responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar participante");
                return StatusCode(500, new { message = "Ocurrió un error al registrar el participante." });
            }
        }
    }
}
