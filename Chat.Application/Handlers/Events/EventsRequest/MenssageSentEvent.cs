using MediatR;

namespace Chat.Application.Handlers.Events.EventsRequest
{
    public class MenssageSentEvent : INotification
    {
        public MenssageSentEvent(int userId, string user, int chatId, string message)
        {
            UserId = userId;
            User = user;
            ChatId = chatId;
            Message = message;
        }

        public int UserId { get; }
        public string User { get;  }
        public int ChatId { get;  }
        public string Message { get;  }
    }
}
