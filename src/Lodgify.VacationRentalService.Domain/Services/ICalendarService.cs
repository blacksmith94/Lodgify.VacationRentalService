using Lodgify.VacationRentalService.Domain.Models;

namespace Lodgify.VacationRentalService.Domain.Services
{
	public interface ICalendarService
	{
		public Calendar Get(CalendarRequest calendarRequest);
	}
}
