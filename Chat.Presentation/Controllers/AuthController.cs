using Chat.DataContracts.Auth.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _mediator.Send(request);

            if (response is null) return BadRequest("Invalid user or password");

            return Ok(response);
        }
    }
}
