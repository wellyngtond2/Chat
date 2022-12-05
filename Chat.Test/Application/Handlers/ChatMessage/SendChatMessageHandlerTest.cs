using Chat.Application.Handlers.Commands.ChatMessage;
using Chat.DataContracts.Auth.Request;
using Chat.DataContracts.ChatMessage.Request;
using Chat.Domain.Exceptions;
using Chat.Test.Helpers;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Chat.Test.Application.Handlers.ChatMessage
{
    public class SendChatMessageHandlerTest : BaseCommandHandlerTest<SendChatMessageHandler, SendChatMessageRequest, Unit>
    {
        private int ChatRoomId = 1;
        private string Message = "Message";
        public SendChatMessageHandlerTest(MapperFixture mapperFixture) : base(mapperFixture)
        {
            CommandHandler = new SendChatMessageHandler(_validatorsMock, _loggerMock, _mapper, _apiContextMock, _userContext, _mediatorMok);
            Request = new SendChatMessageRequest(ChatRoomId, Message);
        }

        [Fact]
        public async Task Should_ReturnDefault_When_EverethingIsOk()
        {
            //Arrange

            //Act

            await Act();

            //Assert

            Response.Should().NotBeNull();
            Response.Should().Be(default);
        }

        [Fact]
        public async Task Should_ReturnDefault_When_MessageIsNull()
        {
            //Arrange
            Request = new SendChatMessageRequest(ChatRoomId, null);

            //Act

            await Act();

            //Assert

            Response.Should().NotBeNull();
            Response.Should().Be(default);
        }

        [Fact]
        public async Task Should_ReturnDefault_When_MessageIsACommand()
        {
            //Arrange
            Request = new SendChatMessageRequest(ChatRoomId, "/stock=xyz");

            //Act

            await Act();

            //Assert

            Response.Should().NotBeNull();
            Response.Should().Be(default);
        }

        [Fact]
        public async Task Should_ReturnException_When_MessageCommandIsIvalid()
        {
            //Arrange
            Request = new SendChatMessageRequest(ChatRoomId, "/stock=");

            //Act & Assert

            await Assert.ThrowsAsync<InvalidStockCodeException>(() => Act());
        }
    }
}
