using System.ComponentModel.DataAnnotations;
namespace Congress_Backend.Models
{
    public class Participante
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string TwitterUser { get; set; }
        [Required]
        [MaxLength(100)]
        public string Occupation {  get; set; }
        [Required]
        public string AvatarURL { get; set; }
    }
}
