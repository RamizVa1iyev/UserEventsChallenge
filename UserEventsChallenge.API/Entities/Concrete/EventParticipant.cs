using UserEventsChallenge.API.Entities.Abstract;

namespace UserEventsChallenge.API.Entities.Concrete;

public class EventParticipant:IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; }

    public EventParticipant()
    {
        
    }

    public EventParticipant(int userId,int eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}
