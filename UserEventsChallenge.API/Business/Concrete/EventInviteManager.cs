using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Business.Caching;
using UserEventsChallenge.API.DataAccess.Abstract;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Enum;
using UserEventsChallenge.API.Entities.Exceptions;

namespace UserEventsChallenge.API.Business.Concrete;

public class EventInviteManager : IEventInviteManager
{
    private readonly IEventInviteDal _eventInviteDal;
    private readonly IEventParticipantDal _eventParticipantDal;
    private readonly ICacheManager _cacheManager;

    public EventInviteManager(IEventInviteDal eventInviteDal, IEventParticipantDal eventParticipantDal, ICacheManager cacheManager)
    {
        _eventInviteDal = eventInviteDal;
        _eventParticipantDal = eventParticipantDal;
        _cacheManager = cacheManager;
    }

    public async Task AcceptInviteAsync(int userId, int eventId)
    {
        var invite = await _eventInviteDal.GetAsync(i => i.UserId == userId & i.EventId == eventId && i.Status == InviteStatus.Waiting) ?? throw new BusinessException("InviteNotFound");


        var participant = await _eventParticipantDal.GetAsync(p => p.UserId == invite.UserId && p.EventId == invite.EventId);

        if (participant is not null)
            throw new BusinessException("AlreadyParticipantToThatEvent");

        _cacheManager.RemoveByPattern($"EventParticipants.GetListAsync?EventId={invite.EventId}");

        await _eventInviteDal.AcceptInvite(invite);
    }

    public async Task<List<EventInviteDto>> GetUserInvitesAsync(int userId)
    {
        var result = await _eventInviteDal.GetUserInvitesAsync(userId);

        return result;
    }

    public async Task InviteUsersAsync(InviteUserDto inviteUserDto)
    {
        var eventInvites = new List<EventInvite>(inviteUserDto.UserIds.Count);
        for (int i = 0;i<inviteUserDto.UserIds.Count;i++)
        {
            eventInvites.Add(new EventInvite(inviteUserDto.EventId, inviteUserDto.UserIds[i],InviteStatus.Waiting));
        }

        await _eventInviteDal.AddRangeAsync(eventInvites);
    }

    public async Task RejectInviteAsync(int userId, int eventId)
    {
        var invite = await _eventInviteDal.GetAsync(i => i.UserId == userId & i.EventId == eventId&&i.Status==InviteStatus.Waiting) ?? throw new BusinessException("InviteNotFound");

        invite.Status = InviteStatus.Rejected;

        await _eventInviteDal.UpdateAsync(invite);
    }
}
