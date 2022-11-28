using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Services;
using Chat.DataContracts.Auth.Request;
using Chat.DataContracts.Auth.Response;
using Chat.DataContracts.Base;
using Chat.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Chat.Application.Handlers.Commands.Auth
{
    public class AuthenticateHandler : BaseCommandHandler<AuthenticateRequest, BaseResponse<TokenResponse>>
    {
        private readonly Infrastructure.Context.ApiContext _dbContext;
        private readonly ITokenService _tokenService;
        public AuthenticateHandler(IEnumerable<IValidator<AuthenticateRequest>> validators, ILogger logger, IMapper mapper, Infrastructure.Context.ApiContext dbContext, ITokenService tokenService) : base(validators, logger, mapper)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        protected override async Task<BaseResponse<TokenResponse>> Process(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var password = request.Password;

            var membership = await _dbContext.Memberships.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == password, cancellationToken);

            if (membership is null)
            {
                return default;
            }

            var token = await _tokenService.GetUserTokenAsync(membership);

            return BaseResponse<TokenResponse>.Create(token);

        }
    }
}
