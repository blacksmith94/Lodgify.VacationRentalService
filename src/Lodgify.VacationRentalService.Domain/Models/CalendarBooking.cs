using Lodgify.VacationRentalService.Domain.Models.Interfaces;

namespace Lodgify.VacationRentalService.Domain.Models
{
	public class CalendarBooking : IBookingPeriod
	{
		public int Id { get; set; }

		public int Unit { get; set; }
	}
}
