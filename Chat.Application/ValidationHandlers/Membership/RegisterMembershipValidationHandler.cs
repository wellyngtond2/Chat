using Chat.DataContracts.Membership.Request;
using Chat.Infrastructure.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.ValidationHandlers.Membership
{
    public class RegisterMembershipValidationHandler : AbstractValidator<RegisterMembershipRequest>
    {
        private readonly IApiContext _dbContext;
        public RegisterMembershipValidationHandler(IApiContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(req => req.Email)
               .NotEmpty()
               .NotNull()
               .EmailAddress()
               .CustomAsync(AlreadyExists);

            RuleFor(req => req.Name)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);

            RuleFor(req => req.Password)
               .NotEmpty()
               .NotNull()
               .MinimumLength(8);

            RuleFor(req => req.ConfirmPassword)
               .NotEmpty()
               .NotNull()
               .MinimumLength(8);

            RuleFor(req => req)
               .NotEmpty()
               .NotNull()
               .Custom(ValidatePasswords);
        }

        private async Task AlreadyExists(string email, ValidationContext<RegisterMembershipRequest>  validationContext, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Memberships.Where(x => x.Email == email).AnyAsync(cancellationToken);

            if (user)
            {
                validationContext.AddFailure(nameof(email), "Email is already being used.");
                return;
            }
        }

        private void ValidatePasswords(RegisterMembershipRequest request, ValidationContext<RegisterMembershipRequest> validationContext)
        {            
            if(!request.Password.Equals(request.ConfirmPassword))
            {
                validationContext.AddFailure(nameof(request.Password), "Password and ConfirmPassword must be the same.");
                return;
            }
        }
    }
}
