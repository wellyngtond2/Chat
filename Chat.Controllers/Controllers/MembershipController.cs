using Chat.DataContracts.Membership.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterMembershipRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
