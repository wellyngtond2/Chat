using AutoMapper;
using Chat.DataContracts.ChatMessage.Request;
using Chat.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SignalRHub(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task SendMessageToChat(int chatId, string message)
        {
            var request = new SendChatMessageRequest(chatId, message);
            await _mediator.Send(request);
        }
        public async Task GetMessagesByChatIDAsync(int chatId)
        {
            var request = new GetChatMessagesRequest(chatId);
            var response = await _mediator.Send(request);

            var messages = _mapper.Map<ICollection<HubMessageDto>>(response);

            await Clients.All.SendAsync("GetMessagesByChatIDAsync", messages);
        }
    }
}
