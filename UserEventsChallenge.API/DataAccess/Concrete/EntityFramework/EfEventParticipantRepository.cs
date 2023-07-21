using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework.Contexts;
using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.DataAccess.Abstract;

namespace UserEventsChallenge.API.DataAccess.Concrete.EntityFramework;

public class EfEventParticipantRepository : EfRepositoryBase<EventParticipant, UserEventsDbContext>, IEventParticipantDal
{
    public EfEventParticipantRepository(UserEventsDbContext context) : base(context)
    {
    }
}
