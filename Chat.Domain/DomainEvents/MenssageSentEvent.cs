using Chat.Share.Events.Interfaces;

namespace Chat.Domain.DomainEvents
{
    public class MenssageSentEvent : IDomainEvents
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
