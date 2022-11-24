using MediatR;

namespace Chat.DataContracts.ChatRoom.Request
{
    public sealed class CreateChatRoomRequest : IRequest
    {
        public string Name { get; set; }
    }
}
