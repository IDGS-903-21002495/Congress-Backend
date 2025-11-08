namespace Congress_Backend.DTOs
{
    public class ListParticipantesDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TwitterUser { get; set; }
        public string Occupation { get; set; }
        public string AvatarURL { get; set; }
    }
}
