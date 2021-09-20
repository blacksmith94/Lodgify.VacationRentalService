using FluentValidation;
using Lodgify.VacationRentalService.WebAPI.DTOs;

namespace Lodgify.VacationRentalService.WebAPI.Validation
{
	/// <summary>
	/// This class will validate a Calendar request payload using Fluent Validator, 
	/// </summary>
	public class CalendarRequestValidator : AbstractValidator<CalendarRequestDTO>
	{
		public CalendarRequestValidator()
		{
			RuleFor(calendarRequest => calendarRequest.Nights).Must((nights) => IsPositive(nights)).WithMessage($"Nights must be positive").WithErrorCode("1");
		}

		/// <summary>
		/// Validates that the given number is a positive number.
		/// <param name="number"> number to validate </param>
		/// </summary>
		private bool IsPositive(int number)
		{
			return number >= 0;
		}
	}
}
