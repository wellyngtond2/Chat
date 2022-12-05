using Chat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Test.Helpers
{
    internal class DataBaseContextMock
    {
        public ApiContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApiContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
