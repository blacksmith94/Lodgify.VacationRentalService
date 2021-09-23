using Lodgify.VacationRentalService.Domain.Models;
using System.Collections.Generic;
using System.Net;

namespace Lodgify.VacationRentalService.Domain.Services
{
	/// <summary>
	/// This class will be registered as a service and will handle the domain's Calendar logic
	/// </summary>
	public class CalendarService : ICalendarService
	{
		
		private readonly IBookingService bookingService;
		private readonly IRentalService rentalService;

		public CalendarService(IBookingService bookingService,
							   IRentalService rentalService)
		{
			this.bookingService = bookingService;
			this.rentalService = rentalService;
		}

		public Calendar Get(CalendarRequest calendarRequest)
		{
			if (rentalService.GetById(calendarRequest.RentalId) == null)
				throw new HttpException(HttpStatusCode.NotFound, "Rental not found");

			var result = new Calendar
			{
				RentalId = calendarRequest.RentalId,
				Dates = new List<CalendarDate>()
			};

			for (var i = 0; i < calendarRequest.Nights; i++)
			{
				var calendarDate = new CalendarDate
				{
					Date = calendarRequest.Start.Date.AddDays(i),
					Bookings = new List<CalendarBooking>(),
					PreparationTimes = new List<CalendarBooking>()
				};

				var bookings = bookingService.GetByDateAndId(calendarRequest.RentalId, calendarDate.Date);
				foreach (var booking in bookings)
				{
					if (booking.IsPreparationTime)
					{
						calendarDate.PreparationTimes.Add(new CalendarBooking { Id = booking.Id, Unit = booking.Unit });
					}
					else
					{
						calendarDate.Bookings.Add(new CalendarBooking { Id = booking.Id, Unit = booking.Unit });
					}
				}
				result.Dates.Add(calendarDate);
			}

			return result;
		}

	}
}