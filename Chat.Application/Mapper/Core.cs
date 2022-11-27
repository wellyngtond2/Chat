using AutoMapper;
using Chat.DataContracts.Membership.Request;
using Chat.Domain.Entities;

namespace Chat.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            MembershipMap();
        }

        private void MembershipMap()
        {
            CreateMap<RegisterMembershipRequest, Membership>();
        }
    }
}
