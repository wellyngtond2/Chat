using Chat.DataContracts.ChatMessage.Request;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetChatRoomRequest();
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{chatId}/message")]
        public async Task<IActionResult> SendMessage(int chatId)
        {
            var request = new GetChatMessagesRequest(chatId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(SendChatMessageRequest request)
        {
            if (request is null) return BadRequest();

            await _mediator.Send(request);

            return Ok();
        }
    }
}
