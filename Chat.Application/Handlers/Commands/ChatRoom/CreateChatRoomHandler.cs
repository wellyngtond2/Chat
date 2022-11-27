using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatRoom.Request;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.ChatRoom
{
    public class CreateChatRoomHandler : BaseCommandHandler<CreateChatRoomRequest, Unit>
    {
        public CreateChatRoomHandler(IEnumerable<IValidator<CreateChatRoomRequest>> validators, ILogger logger, IMapper mapper) : base(validators, logger, mapper)
        {
        }

        protected override Task<Unit> Process(CreateChatRoomRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
