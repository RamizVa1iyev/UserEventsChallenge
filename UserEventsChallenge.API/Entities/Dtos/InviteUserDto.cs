namespace UserEventsChallenge.API.Entities.Dtos
{
    public class InviteUserDto
    {
        public int EventId { get; set; }

        public List<int> UserIds { get; set; }
    }
}
