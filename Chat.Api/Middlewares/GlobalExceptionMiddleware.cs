using Chat.DataContracts.Base;
using Chat.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is ValidationException)
            {
                var validations = (exception as ValidationException).Errors.Select(err => new ValidationResponse(err.PropertyName, err.ErrorMessage));

                var validationResponse = new BaseResponse<ValidationResponse[]> { Data = validations.ToArray() };

                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync(new JsonResult(validationResponse) { StatusCode = 400 });

                return;
            }

            var error = new ErrorResponse("Internal Server Error");

            var errorResponse = new BaseResponse<ErrorResponse> { Data = error };

            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new JsonResult(errorResponse) { StatusCode = 500 });

            return;

        }
    }
}
