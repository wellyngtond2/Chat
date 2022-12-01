using Chat.Share.Events.Interfaces;
using FluentValidation.Results;

namespace Chat.Share.Events
{
    public class ResponseMessage : IMessage
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}
