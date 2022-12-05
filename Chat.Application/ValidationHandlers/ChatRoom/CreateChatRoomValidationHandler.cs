using Chat.DataContracts.ChatRoom.Request;
using FluentValidation;

namespace Chat.Application.ValidationHandlers.ChatRoom
{
    public class CreateChatRoomValidationHandler : AbstractValidator<CreateChatRoomRequest>
    {
        public CreateChatRoomValidationHandler()
        {
            RuleFor(req => req.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);

        }
    }
}
