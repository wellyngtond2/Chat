using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.DataContracts.ChatRoom.Request;
using Chat.DataContracts.ChatRoom.Response;
using Chat.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Handlers.Queries.ChatRoom
{
    public class GetChatRoomHandler : BaseCommandHandler<GetChatRoomRequest, ICollection<ChatRoomResponse>>
    {
        private readonly Infrastructure.Context.IApiContext _dbContext;
        public GetChatRoomHandler(IEnumerable<IValidator<GetChatRoomRequest>> validators, ILogger logger, IMapper mapper, Infrastructure.Context.IApiContext dbContext) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
        }

        protected async override Task<ICollection<ChatRoomResponse>> Process(GetChatRoomRequest request, CancellationToken cancellationToken)
        {
            var chatRooms = await _dbContext.ChatRooms.ToListAsync(cancellationToken);

            var response = _mapper.Map<ICollection<ChatRoomResponse>>(chatRooms);

            return response;
        }
    }
}
