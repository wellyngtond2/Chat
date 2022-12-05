
using Chat.DataContracts.Auth.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.ValidationHandlers.Auth
{
    public class AuthenticateValidationHandler : AbstractValidator<AuthenticateRequest>
    {
        private readonly ApiContext _dbContext;

        public AuthenticateValidationHandler(ApiContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(req => req.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .CustomAsync(UserExists);

            RuleFor(req => req.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(8);
        }

        private async Task UserExists(string email, ValidationContext<AuthenticateRequest> validationContext, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Memberships.Where(x => x.Email == email).AnyAsync(cancellationToken);

            if(!user)
            {
                validationContext.AddFailure(nameof(email), "Ivalid user or password");
                return;
            }
        }
    }
}
