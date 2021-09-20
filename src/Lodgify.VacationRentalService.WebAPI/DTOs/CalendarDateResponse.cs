using System;
using System.Collections.Generic;

namespace Lodgify.VacationRentalService.WebAPI.DTOs
{
	public class CalendarDateResponse
	{
		public DateTime Date { get; set; }
		public List<CalendarBookingResponse> Bookings { get; set; }
		public List<PreparationTimeResponse> PreparationTimes { get; set; }
	}
}
