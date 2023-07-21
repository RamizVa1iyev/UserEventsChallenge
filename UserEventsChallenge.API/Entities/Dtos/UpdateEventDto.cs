namespace UserEventsChallenge.API.Entities.Dtos;

public class UpdateEventDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string TimeZoneId { get; set; }
}
