using FluentValidation;
using Lodgify.VacationRentalService.WebAPI.DTOs;

namespace Lodgify.VacationRentalService.WebAPI.Validation
{
	/// <summary>
	/// This class will validate a Rental request DTO using Fluent Validator, 
	/// </summary>
	public class RentalRequestValidator : AbstractValidator<RentalRequestDTO>
	{
		public RentalRequestValidator()
		{
			RuleFor(calendarRequest => calendarRequest.PreparationTimeInDays).Must((prepTime) => IsPositive(prepTime)).WithMessage($"Preparation time must be positive").WithErrorCode("1");
			RuleFor(calendarRequest => calendarRequest.Units).Must((units) => IsGreaterThanZero(units)).WithMessage($"Units must be greater than 0").WithErrorCode("2");
		}

		/// <summary>
		/// Validates that the given number is a positive number.
		/// <param name="number"> number to validate </param>
		/// </summary>
		private bool IsPositive(int number)
		{
			return number >= 0;
		}

		/// <summary>
		/// Validates that the given number is greater than 0.
		/// <param name="number"> number to validate </param>
		/// </summary>
		private bool IsGreaterThanZero(int number)
		{
			return number > 0;
		}
	}
}
