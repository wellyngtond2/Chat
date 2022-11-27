using Chat.DataContracts.ChatRoom.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChatRoomRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetChatRoomRequest();
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
