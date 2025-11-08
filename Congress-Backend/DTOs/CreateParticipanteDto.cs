namespace Congress_Backend.DTOs
{
    public class CreateParticipanteDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TwitterUser {  get; set; }
        public string Occupation { get; set; }
        public string AvatarURL { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
