using Chat.DataContracts.ChatRoom.Response;
using MediatR;

namespace Chat.DataContracts.ChatRoom.Request
{
    public sealed class GetChatRoomRequest : IRequest<ChatRoomResponse>
    {
        public GetChatRoomRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
