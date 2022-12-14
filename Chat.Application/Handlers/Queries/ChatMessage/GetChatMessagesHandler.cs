using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatMessage.Request;
using Chat.DataContracts.ChatMessage.Response;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Chat.Application.Handlers.Queries.ChatMessage
{
    public class GetChatMessagesHandler : BaseCommandHandler<GetChatMessagesRequest, ICollection<ChatMessageResponse>>
    {
        private readonly Infrastructure.Context.IApiContext _dbContext;
        public GetChatMessagesHandler(IEnumerable<IValidator<GetChatMessagesRequest>> validators, ILogger logger, IMapper mapper, Infrastructure.Context.IApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected override async Task<ICollection<ChatMessageResponse>> Process(GetChatMessagesRequest request, CancellationToken cancellationToken)
        {
            var chatMessages = (from cm in _dbContext.ChatMessages
                                join m in _dbContext.Memberships on cm.Creator.Id equals m.Id
                                where cm.ChatRoomId == request.ChatId
                                orderby cm.CreatedAt
                                select new ChatMessageResponse
                                {
                                    MembershipId = m.Id,
                                    MembershipName = m.Name,
                                    Date = cm.CreatedAt,
                                    Message = cm.Message
                                }).Take(50);

            return await chatMessages.ToListAsync(cancellationToken);
        }
    }
}
