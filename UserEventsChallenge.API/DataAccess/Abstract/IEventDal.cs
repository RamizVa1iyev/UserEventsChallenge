using Core.Persistence.Repositories;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Concrete;

public interface IEventDal:IAsyncRepository<Event>,IRepository<Event>
{
}
