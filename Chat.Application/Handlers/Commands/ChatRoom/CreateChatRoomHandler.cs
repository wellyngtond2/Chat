using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Services;
using Chat.DataContracts.ChatRoom.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Chat.Application.Handlers.Commands.ChatRoom
{
    public class CreateChatRoomHandler : BaseCommandHandler<CreateChatRoomRequest, Unit>
    {
        private readonly ApiContext _dbContext;
        private readonly IUserContext _userContext;
        public CreateChatRoomHandler(IEnumerable<IValidator<CreateChatRoomRequest>> validators, ILogger logger, IMapper mapper, ApiContext dbContext, IUserContext userContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        protected async override Task<Unit> Process(CreateChatRoomRequest request, CancellationToken cancellationToken)
        {
            var chatRoom = _mapper.Map<Domain.Entities.ChatRoom>(request);
            chatRoom.SetCreator(_userContext.GetUserContext().userId);
            _dbContext.Add(chatRoom);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
