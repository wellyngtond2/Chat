using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatRoom.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Serilog;

namespace Chat.Application.Handlers.Commands.ChatRoom
{
    public class CreateChatRoomHandler : BaseCommandHandler<CreateChatRoomRequest, Unit>
    {
        private readonly ApiContext _dbContext;
        public CreateChatRoomHandler(IEnumerable<IValidator<CreateChatRoomRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected async override Task<Unit> Process(CreateChatRoomRequest request, CancellationToken cancellationToken)
        {
            var chatRoom = _mapper.Map<Domain.Entities.ChatRoom>(request);

            _dbContext.Add(chatRoom);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
