namespace UserEventsChallenge.API.Entities.Dtos;

public class EventDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string TimeZoneId { get; set; }

    public string TimeZone { get; set; }
}
