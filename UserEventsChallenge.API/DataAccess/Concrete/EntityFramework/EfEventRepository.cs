using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework.Contexts;
using UserEventsChallenge.API.DataAccess.Concrete;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Abstract.EntityFramework
{
    public class EfEventRepository : EfRepositoryBase<Event, UserEventsDbContext>,IEventDal
    {
        public EfEventRepository(UserEventsDbContext context) : base(context)
        {
        }
    }
}
