using Lodgify.VacationRentalService.Domain.Models.Interfaces;

namespace Lodgify.VacationRentalService.WebAPI.DTOs
{
	public class CalendarBookingResponse : IBookingPeriod
	{
		public int Id { get; set; }

		public int Unit { get; set; }
	}
}
