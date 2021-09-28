using Lodgify.VacationRentalService.Domain.Models;

namespace Lodgify.VacationRentalService.Domain.Services.Interfaces
{
	public interface ICalendarService
	{
		public Calendar Get(CalendarRequest calendarRequest);
	}
}
