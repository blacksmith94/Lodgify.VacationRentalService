using Lodgify.VacationRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services.Interfaces
{
	public interface IBookingService
	{
		public Booking GetById(int bookingId);

		public Task<Booking> AddAsync(Booking bookingToAdd, int rentalUnits, int rentalPrepTime);

		public IQueryable<Booking> GetByIdAndDateRange(int rentalId, DateTime date, int nights);

		public IQueryable<Booking> GetBookingsByDateRange(int rentalId, DateTime date, int nights);

		public bool IsBookedByRentalUnitAndDate(int rentalId, int unit, DateTime date);

		public void UpdatePreparationTimes(IEnumerable<Booking> preparationTimes, int preparationTimeInDays);

		public IQueryable<Booking> GetPreparationTimesByRentalAndDate(int rentaId, DateTime date);

		public IQueryable<Booking> GetBookingsByDateRangeAndUnit(int rentalId, int unit, DateTime date, int nights);
	}
}
