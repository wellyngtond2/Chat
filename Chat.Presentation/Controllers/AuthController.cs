using Chat.DataContracts.Auth.Request;
using Chat.Domain.Dtos;
using Chat.Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        IHubContext<SignalRHub> _hubContext;

        public AuthController(IMediator mediator, IHubContext<SignalRHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _mediator.Send(request);

            if (response is null) return BadRequest("Invalid user or password");

            return Ok(response);
        }

        [HttpPost("tes")]
        public async Task<IActionResult> AuthenticateX(AuthenticateRequest request)
        {
            var x = new HubChatMessageDto(1,"x",1,"olá x");

            await _hubContext.Clients.All.SendAsync("ReceiveMessage",x);



            return Ok();
        }
    }
}
