using Core.Persistence.Repositories;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Abstract;

public interface IEventParticipantDal : IAsyncRepository<EventParticipant>, IRepository<EventParticipant>
{
}
