using Core.Persistence.Repositories;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;

namespace UserEventsChallenge.API.DataAccess.Abstract;

public interface IEventInviteDal : IAsyncRepository<EventInvite>, IRepository<EventInvite>
{
    Task AcceptInvite(EventInvite invite);
    Task<List<EventInviteDto>> GetUserInvitesAsync(int userId);
}
