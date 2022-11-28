using Chat.DataContracts.ChatRoom.Response;
using MediatR;

namespace Chat.DataContracts.ChatRoom.Request
{
    public sealed class GetChatRoomRequest : IRequest<ICollection<ChatRoomResponse>>
    {
        public GetChatRoomRequest() { }
    }
}
