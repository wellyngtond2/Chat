using AutoMapper;
using Chat.DataContracts.ChatMessage.Request;
using Chat.DataContracts.ChatMessage.Response;
using Chat.DataContracts.ChatRoom.Request;
using Chat.DataContracts.ChatRoom.Response;
using Chat.DataContracts.Membership.Request;
using Chat.DataContracts.Membership.Response;
using Chat.Domain.Dtos;
using Chat.Domain.Entities;

namespace Chat.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            MembershipMap();
            ChatMap();
            ChatRoomMap();
        }

        private void MembershipMap()
        {
            CreateMap<RegisterMembershipRequest, Membership>();
            CreateMap<Membership, MembershipResponse>();
        }
        private void ChatMap()
        {
            CreateMap<SendChatMessageRequest, Domain.Entities.ChatMessage>();
        }
        private void ChatRoomMap()
        {
            CreateMap<CreateChatRoomRequest, ChatRoom>();
            CreateMap<ChatRoom, ChatRoomResponse>();
        }
    }
}
