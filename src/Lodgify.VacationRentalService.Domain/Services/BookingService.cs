using Lodgify.VacationRentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services
{
	/// <summary>
	/// This class will be registered as a service and will handle the domain's Booking logic
	/// </summary>
	public class BookingService : IBookingService
	{
		private readonly IRepository<Booking> bookingRepoitory;

		/// <summary>
		/// Booking Service constructor.
		/// </summary>
		/// <param name="repository">Injected the repository for the Booking table </param>
		public BookingService(IRepository<Booking> repository)
		{
			this.bookingRepoitory = repository;
		}
		public Booking GetById(int bookingId)
		{
			var query = this.bookingRepoitory.Query;
			var booking = query.FirstOrDefault(b => b.Id == bookingId);
			return booking;
		}

		/// <summary>
		/// Task that stores a booking in DB.
		/// </summary>
		/// <param name="bookingToAdd">Booking model to add.</param>
		/// <param name="rentalUnits">Units that the rental holds</param>
		/// <param name="rentalPrepTime">Rental preparation time</param>
		/// <returns>A Task with with the added model</returns>
		public async Task<Booking> AddAsync(Booking bookingToAdd, int rentalUnits, int rentalPrepTime)
		{
			//Get booked units during that date
			var bookings = GetBookingsByDateRange(bookingToAdd.RentalId, bookingToAdd.Start, bookingToAdd.Nights);
			var bookedUnits = bookings.Select(booking => booking.Unit).Distinct();

			//Check if there are available units
			var bookingUnit = 0;
			var unit = 1;
			while (bookingUnit == 0 && unit <= rentalUnits)
			{
				if (!bookedUnits.Contains(unit))
				{
					bookingUnit = unit;
				}
				unit++;
			}

			if (bookingUnit == 0) throw new HttpException(HttpStatusCode.BadRequest, "Not available");

			bookingToAdd.Unit = bookingUnit;

			await this.bookingRepoitory.Add(bookingToAdd);

			var preparationTime = new Booking()
			{
				Nights = rentalPrepTime,
				RentalId = bookingToAdd.RentalId,
				Start = bookingToAdd.Start.AddDays(bookingToAdd.Nights),
				Unit = bookingUnit,
				IsPreparationTime = true
			};

			await this.bookingRepoitory.Add(preparationTime);

			await this.bookingRepoitory.Save();
			return bookingToAdd;
		}

		/// <summary>
		/// This method will filter the bookings in the DB by rental Id and a Date Range
		/// </summary>
		/// <param name="rentalId"> Id of the rental to filter.</param>
		/// <param name="startDate"> Start date of the booking.</param>
		/// <param name="nights"> Number of nights to determine the end day.</param>
		/// <returns>A queryable collection of bookings that match the filter </returns>
		public IQueryable<Booking> GetBookingsByDateRange(int rentalId, DateTime startDate, int nights)
		{
			var endDate = startDate.AddDays(nights);
			return this.bookingRepoitory.Query.Where(booking =>
				booking.RentalId == rentalId
				&& ((booking.Start <= startDate && booking.Start.AddDays(booking.Nights) > startDate)
				|| (booking.Start < endDate && booking.Start.AddDays(booking.Nights) >= endDate)
				|| (booking.Start > startDate && booking.Start.AddDays(booking.Nights) < endDate)));
		}

		/// <summary>
		/// This method will filter the bookings by date and rental Id, will retrieve both Booking and PreparationTimes
		/// </summary>
		/// <param name="rentalId"> Id of the rental to filter.</param>
		/// <param name="date"> Date of the booking.</param>
		/// <returns>A queryable collection of bookings that match the filter </returns>
		public IQueryable<Booking> GetByDateAndId(int rentalId, DateTime date)
		{
			return this.bookingRepoitory.Query.Where(
				booking => booking.RentalId == rentalId
				&& booking.Start <= date
				&& booking.Start.AddDays(booking.Nights) > date);
		}

		/// <summary>
		/// This method will check wether a given unit of a given rental is booked in a given date
		/// </summary>
		/// <param name="rentalId"> Id of the rental to filter.</param>
		/// <param name="unit"> Unit of the rental.</param>
		/// <param name="date"> Date of the booking.</param>
		/// <returns>True/False </returns>
		public bool IsBookedByRentalUnitAndDate(int rentalId, int unit, DateTime date)
		{
			return this.bookingRepoitory.Query.Where(booking =>
				booking.RentalId == rentalId
				&& booking.Unit == unit
				&& (booking.Start > date || booking.Start <= date && booking.Start.AddDays(booking.Nights) > date))
				.Count() > 0;
		}

		/// <summary>
		/// This method will get the Preparation Times of a given rental in a given date
		/// </summary>
		/// <param name="rentalId"> Id of the rental to filter.</param>
		/// <param name="date"> Date of the preparation time.</param>
		/// <returns>A queryable collection of PreparationTimes that match the filter </returns>
		public IQueryable<Booking> GetPreparationTimesByRentalAndDate(int rentalId, DateTime date)
		{
			return this.bookingRepoitory.Query.Where(booking =>
				booking.RentalId == rentalId
				&& (booking.Start > date || booking.Start <= date && booking.Start.AddDays(booking.Nights) > date)
				&& booking.IsPreparationTime);
		}

		/// <summary>
		/// This method will get the Bokings of a given rental, unit and date range
		/// </summary>
		/// <param name="rentalId"> Id of the rental to filter.</param>
		/// <param name="unit"> Unit of the rental.</param>
		/// <param name="date"> Date of the preparation time.</param>
		/// <param name="nights"> Number of nights to determine the end day.</param>
		/// <returns>A queryable collection of PreparationTimes that match the filter </returns>
		public IQueryable<Booking> GetBookingsByDateRangeAndUnit(int rentalId, int unit, DateTime date, int nights)
		{
			var endDate = date.AddDays(nights);
			return this.bookingRepoitory.Query.Where(booking => booking.RentalId == rentalId
				&& booking.Unit == unit
				&& ((booking.Start <= date && booking.Start.AddDays(booking.Nights) > date)
				|| (booking.Start < endDate && booking.Start.AddDays(booking.Nights) >= endDate)
				|| (booking.Start > date && booking.Start.AddDays(booking.Nights) < endDate)));
		}


		/// <summary>
		/// This method will update the preparation time of a booking
		/// </summary>
		/// <param name="preparationTimes"> Collection of preparation times to update.</param>
		/// <param name="preparationTimeInDays"> new preparation time in days.</param>
		public void UpdatePreparationTimes(IEnumerable<Booking> preparationTimes, int preparationTimeInDays)
		{
			foreach (var preparationTime in preparationTimes)
			{
				preparationTime.Nights = preparationTimeInDays;
				bookingRepoitory.Update(preparationTime);
			}
			bookingRepoitory.Save();
		}
	}
}