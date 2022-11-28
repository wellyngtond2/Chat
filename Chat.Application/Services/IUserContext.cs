using Chat.DataContracts.Context;

namespace Chat.Application.Services
{
    public interface IUserContext
    {
        UserDataContext GetUserContext();
    }
}
