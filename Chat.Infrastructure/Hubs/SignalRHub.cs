using AutoMapper;
using Chat.DataContracts.ChatMessage.Request;
using Chat.Domain.Dtos;
using Chat.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Hubs
{
    public class SignalRHub : Hub<IHubChatService>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SignalRHub(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task SendMessageToChat(HubChatMessageDto messageDto)
        {

            var x = Clients.All.ReceiveMessage(messageDto);


        }
    }
}
