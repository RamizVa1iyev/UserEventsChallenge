namespace UserEventsChallenge.API.Entities.Dtos
{
    public class EventParticipantDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}
