using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatRoom.Request;
using Chat.DataContracts.ChatRoom.Response;
using FluentValidation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Handlers.Queries.ChatRoom
{
    public class GetChatRoomHandler : BaseCommandHandler<GetChatRoomRequest, ChatRoomResponse>
    {
        public GetChatRoomHandler(IEnumerable<IValidator<GetChatRoomRequest>> validators, ILogger logger, IMapper mapper) : base(validators, logger, mapper)
        {
        }

        protected override Task<ChatRoomResponse> Process(GetChatRoomRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
