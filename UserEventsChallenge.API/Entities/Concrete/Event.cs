using UserEventsChallenge.API.Entities.Abstract;

namespace UserEventsChallenge.API.Entities.Concrete;

public class Event: IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string TimeZoneId { get; set; }

    public virtual ICollection<EventParticipant> EventParticipants { get; set; }
    public virtual ICollection<EventInvite> EventInvites { get; set; }
}
