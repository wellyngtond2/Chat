using Chat.Infrastructure.Hubs;
using Microsoft.AspNetCore.Builder;

namespace Chat.Presentation.Extensions
{
    public static class AppExtensions
    {
        public static WebApplication UseSignalR(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalRHub>("/chatRoom");
            });

            return app;
        }
    }
}
