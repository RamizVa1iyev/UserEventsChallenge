using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework.Contexts;
using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework;
using UserEventsChallenge.API.DataAccess.Abstract;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using UserEventsChallenge.API.Entities.Enum;

namespace UserEventsChallenge.API.DataAccess.Concrete.EntityFramework;

public class EfEventInviteRepository : EfRepositoryBase<EventInvite, UserEventsDbContext>, IEventInviteDal
{
    public EfEventInviteRepository(UserEventsDbContext context) : base(context)
    {
    }

    public async Task AcceptInvite(EventInvite invite)
    {
        using var transaction = Context.Database.BeginTransaction();

        try
        {

            invite.Status = InviteStatus.Accepted;
            Context.EventInvites.Update(invite);


            var newEventParticipant = new EventParticipant(invite.UserId, invite.EventId);
            await Context.EventParticipants.AddAsync(newEventParticipant);

            await Context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<EventInviteDto>> GetUserInvitesAsync(int userId)
    {
        var result = from invites in Context.EventInvites
                     join events in Context.Events
                     on invites.EventId equals events.Id
                     where invites.UserId == userId && invites.Status == InviteStatus.Waiting
                     select new EventInviteDto()
                     {
                         Id = invites.Id,
                         EventId = events.Id,
                         CreatorUserId = events.UserId
                     };

        return await result.ToListAsync();
    }
}
