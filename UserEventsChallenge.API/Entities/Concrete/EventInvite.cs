using UserEventsChallenge.API.Entities.Abstract;
using UserEventsChallenge.API.Entities.Enum;

namespace UserEventsChallenge.API.Entities.Concrete;

public class EventInvite:IEntity
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public InviteStatus Status { get; set; }

    public virtual Event Event { get; set; }

    public EventInvite()
    {
        
    }

    public EventInvite(int eventId,int userId, InviteStatus status)
    {
        EventId = eventId;
        UserId = userId;
        Status = status;
    }
}
