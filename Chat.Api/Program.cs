using Chat.Api.Middlewares;
using Chat.Infrastructure.Context;
using Chat.Infrastructure.Hubs;
using Chat.Presentation.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(Chat.Presentation.AssemblyReference).Assembly;

builder.Services.AddSignalR();

builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services.RegisterAuthenticate(builder.Configuration);

builder.Services.RegisterSettings(builder.Configuration);

builder.Services.RegisterCore(builder.Configuration);

builder.Services.AddDbContext<ApiContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.RegisterDomainServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterSwagger();

builder.Services.AddTransient<GlobalExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.UseSignalR();

app.Run();
