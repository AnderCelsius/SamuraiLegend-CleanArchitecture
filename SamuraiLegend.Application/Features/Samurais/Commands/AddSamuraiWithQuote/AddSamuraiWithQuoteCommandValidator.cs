using FluentValidation;
using SamuraiLegend.Application.Features.Samurais.Commands.AddSamuraiWithQuote;
using SamuraiLegend.Application.Interfaces.Repositories;
using SamuraiLegend.Application.Validations;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Features.Samurais.Commands.CreateSamuraiWithQuote
{
    public class AddSamuraiWithQuoteCommandValidator : AbstractValidator<AddSamuraiWithQuoteCommand>
    {
        private readonly ISamuraiRepository _samuraiRepository;

        public AddSamuraiWithQuoteCommandValidator(ISamuraiRepository samuraiRepository)
        {
            _samuraiRepository = samuraiRepository;

            RuleFor(s => s.Name).HumanName()
                .MustAsync(IsUniqueName).WithMessage("{PropertyName} already exists.");

            RuleFor(s => s.Quote)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MinimumLength(20).WithMessage("{PropertyName} must be minimum of 5 words")
                .Matches("^\\s+[A-Za-z,;'\"\\s]+[.?!]$").WithMessage("Are you forgeting a period? Quotes must end with either of '.?!'");

            RuleFor(s => s.ShortStory)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MinimumLength(50).WithMessage("[PropertyName] must be minimum of 50 letters because we love a story");
        }

        private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _samuraiRepository.IsUniqueSamuraiNameAsync(name);
        }
    }
    
}
