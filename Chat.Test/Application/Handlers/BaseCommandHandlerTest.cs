using AutoMapper;
using Chat.Application.Handlers.Base;
using Chat.Application.Services;
using Chat.DataContracts.Context;
using Chat.Infrastructure.Context;
using Chat.Test.Helpers;
using FluentValidation;
using MediatR;
using NSubstitute;
using Serilog;

namespace Chat.Test.Application.Handlers
{
    public class BaseCommandHandlerTest<TCommandHandler, TRequest, TResponse> : IClassFixture<MapperFixture>
       where TCommandHandler : BaseCommandHandler<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        protected readonly IEnumerable<IValidator<TRequest>> _validatorsMock;
        protected readonly ILogger _loggerMock;
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediatorMok;
        protected TRequest Request;
        protected TResponse Response;
        protected TCommandHandler CommandHandler;
        protected readonly IUserContext _userContext;
        protected readonly IApiContext _apiContextMock;

        protected BaseCommandHandlerTest(MapperFixture mapperFixture)
        {
            _validatorsMock = Substitute.For<IEnumerable<IValidator<TRequest>>>();
            _loggerMock = Substitute.For<ILogger>();
            _userContext = Substitute.For<IUserContext>();
            _apiContextMock = Substitute.For<IApiContext>();
            _mediatorMok = Substitute.For<IMediator>();

            _mapper = mapperFixture.Mapper;

            _userContext.GetUserContext().Returns(new UserDataContext(1, "email@email.com", "username"));

        }
        protected async Task Act() => Response = await CommandHandler.Handle(Request, CancellationToken.None);
    }
}
