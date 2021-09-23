using FluentValidation;
using Lodgify.VacationRentalService.WebAPI.DTOs;

namespace Lodgify.VacationRentalService.WebAPI.Validation
{
	/// <summary>
	/// This class will validate a Booking request payload using Fluent Validator, 
	/// </summary>
	public class BookingRequestValidator : AbstractValidator<BookingRequestDTO>
	{
		public BookingRequestValidator()
		{
			RuleFor(bookingRequest => bookingRequest.Nights).Must((nights) => IsPositive(nights)).WithMessage($"Nights must be positive").WithErrorCode("1");
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
