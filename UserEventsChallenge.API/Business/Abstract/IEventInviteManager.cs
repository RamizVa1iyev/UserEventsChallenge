using UserEventsChallenge.API.Entities.Dtos;
namespace UserEventsChallenge.API.Business.Abstract;

public interface IEventInviteManager
{
    Task InviteUsersAsync(InviteUserDto inviteUserDto);
    Task<List<EventInviteDto>> GetUserInvitesAsync(int userId);
    Task AcceptInviteAsync(int userId,int eventId);
    Task RejectInviteAsync(int userId,int eventId);
}
