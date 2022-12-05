using Chat.Application.Handlers.Commands.Auth;
using Chat.Application.Services;
using Chat.DataContracts.Auth.Request;
using Chat.DataContracts.Auth.Response;
using Chat.DataContracts.Base;
using Chat.Domain.Entities;
using Chat.Share.Helpers;
using Chat.Test.Helpers;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Chat.Test.Application.Handlers.Auth;

public class AuthenticateHandlerTest : BaseCommandHandlerTest<AuthenticateHandler, AuthenticateRequest, BaseResponse<TokenResponse>>
{
    private string userName = "user";
    private string userEmail = "teste@gmail.com";
    private string userPassword = "12345678";
    DbSet<Membership>? _membershipDbSet;
    private readonly ITokenService _tokenService;
    public AuthenticateHandlerTest(MapperFixture mapperFixture) : base(mapperFixture)
    {
        _tokenService = Substitute.For<ITokenService>();
        CommandHandler = new AuthenticateHandler(_validatorsMock, _loggerMock, _mapper, _apiContextMock, _tokenService);


        _membershipDbSet = Builder<Membership>
                        .CreateListOfSize(1)
                        .All()
                        .WithFactory(() => new Membership(1, userName, userEmail, SecurityHelper.StringToHash(userPassword)))
                        .Build()
                        .AsQueryable()
                        .BuildMockDbSet();


        var tokenResponse = Builder<TokenResponse>.CreateNew()
            .With(x => x.ExpirationIn = 1)
            .Build();

        _apiContextMock.Memberships.Returns(_membershipDbSet);
        _tokenService.GetUserTokenAsync(_membershipDbSet.FirstOrDefault()).Returns(tokenResponse);
    }

    [Fact]
    public async Task Should_ReturnToken_When_UserAndPassword_AreCorrect()
    {
        //Arrange
        Request = new AuthenticateRequest(userEmail, userPassword);

        //Act

        await Act();

        //Assert

        Response.Data.Token.Should().NotBeNull();
        Response.Data.ExpirationIn.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_ReturnDefaultResponse_When_UserAndPassword_AreInvalid()
    {
        //Arrange
        Request = new AuthenticateRequest(userEmail + "1", userPassword);

        //Act

        await Act();

        //Assert

        Response.Should().Be(default);
    }

    [Fact]
    public async Task Should_ReturnDefaultResponse_When_UserAndPassword_AreNull()
    {
        //Arrange
        Request = new AuthenticateRequest(null,null);

        //Act

        await Act();

        //Assert

        Response.Should().Be(default);
    }
}

