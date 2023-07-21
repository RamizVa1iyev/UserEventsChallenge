using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Entities.Dtos;

namespace UserEventsChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventInvitesController : ControllerBase
    {
        private readonly IEventInviteManager _eventInviteManager;

        public EventInvitesController(IEventInviteManager eventInviteManager)
        {
            _eventInviteManager = eventInviteManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInvites(int userId)
        {
            var result = await _eventInviteManager.GetUserInvitesAsync(userId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InviteUsers(InviteUserDto inviteUserDto)
        {
            await _eventInviteManager.InviteUsersAsync(inviteUserDto);

            return Ok();
        }

        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptInvite(int userId,int eventId)
        {
            await _eventInviteManager.AcceptInviteAsync(userId,eventId);

            return Ok();
        }

        [HttpPost("Reject")]
        public async Task<IActionResult> RejectInvite(int userId, int eventId)
        {
            await _eventInviteManager.RejectInviteAsync(userId, eventId);

            return Ok();
        }
    }
}
