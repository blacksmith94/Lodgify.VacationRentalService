using System.Collections.Generic;

namespace Lodgify.VacationRentalService.WebAPI.DTOs
{
	public class CalendarResponse
	{
		public int RentalId { get; set; }
		public List<CalendarDateResponse> Dates { get; set; }
	}
}
