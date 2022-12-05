using Chat.DataContracts.ChatMessage.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.ValidationHandlers.ChatMessage
{
    public class SendChatMessageValidationHandler : AbstractValidator<SendChatMessageRequest>
    {
        private readonly IApiContext _dbContext;
        public SendChatMessageValidationHandler(IApiContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(req => req.ChatRoomId)
                .NotEmpty()
                .NotNull()
                .CustomAsync(ExistsChatRoom);

            RuleFor(req => req.Message)
                .NotEmpty()
                .NotNull()
                .Custom(ValidateMsg);
        }

        private async Task ExistsChatRoom(int chatRoomId, ValidationContext<SendChatMessageRequest> validationContext, CancellationToken cancellationToken)
        {
            var chatRoom = await _dbContext.ChatRooms.Where(x => x.Id == chatRoomId).AnyAsync(cancellationToken);

            if (!chatRoom)
            {
                validationContext.AddFailure(nameof(chatRoomId), "Ivalid chatRoom");
                return;
            }
        }

        private void ValidateMsg(string message, ValidationContext<SendChatMessageRequest> validationContext)
        {
            if (message.StartsWith("/") && !message.Contains("stock="))
            {
                validationContext.AddFailure(nameof(message), "Invalid message command");
                return;
            }
        }
    }
}
