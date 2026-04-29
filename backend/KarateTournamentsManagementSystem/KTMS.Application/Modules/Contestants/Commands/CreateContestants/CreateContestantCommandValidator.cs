using FluentValidation;

namespace KTMS.Application.Modules.Contestants.Commands.CreateContestants
{
    public sealed class CreateContestantCommandValidator : AbstractValidator<CreateContestantsCommand>
    {
        public CreateContestantCommandValidator()
        {
            RuleFor(x => x.CreateContestantsDto)
                .NotNull()
                .WithMessage("Contestant data is required.");

            RuleFor(x => x.CreateContestantsDto.Name)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MinimumLength(2)
                .WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50)
                .WithMessage("First name can be at most 50 characters long.")
                .Matches(@"^[A-Za-zČĆŽŠĐčćžšđ\s'-]+$")
                .WithMessage("First name can only contain letters.");

            RuleFor(x => x.CreateContestantsDto.Surname)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MinimumLength(2)
                .WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50)
                .WithMessage("Last name can be at most 50 characters long.")
                .Matches(@"^[A-Za-zČĆŽŠĐčćžšđ\s'-]+$")
                .WithMessage("Last name can only contain letters.");

            RuleFor(x => x.CreateContestantsDto.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(30)
                .WithMessage("Phone number can be at most 30 characters long.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$")
                .WithMessage("Phone number is not valid.");

            RuleFor(x => x.CreateContestantsDto.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email is not valid.")
                .MaximumLength(100)
                .WithMessage("Email can be at most 100 characters long.");

            RuleFor(x => x.CreateContestantsDto.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of birth is required.")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.CreateContestantsDto.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(30)
                .WithMessage("Username can be at most 30 characters long.")
                .Matches(@"^[a-zA-Z0-9._-]+$")
                .WithMessage("Username can only contain letters, numbers, dots, underscores and dashes.");

            RuleFor(x => x.CreateContestantsDto.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100)
                .WithMessage("Password can be at most 100 characters long.");

            RuleFor(x => x.CreateContestantsDto.CityId)
                .GreaterThan(0)
                .WithMessage("City is required.");

            RuleFor(x => x.CreateContestantsDto.GenderId)
                .GreaterThan(0)
                .WithMessage("Gender is required.");

            RuleFor(x => x.CreateContestantsDto.BeltId)
                .GreaterThan(0)
                .WithMessage("Belt is required.");

            RuleFor(x => x.CreateContestantsDto.ClubId)
                .GreaterThan(0)
                .WithMessage("Club is required.");
        }
    }
}
