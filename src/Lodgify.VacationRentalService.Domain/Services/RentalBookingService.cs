using Lodgify.VacationRentalService.Domain.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services
{
	/// <summary>
	/// This class will be registered as a service and will handle the logic that involve both Rentals and Bookings
	/// </summary>
	public class RentalBookingService : IRentalBookingService
	{
		private readonly IBookingService bookingService;
		private readonly IRentalService rentalService;

		/// <summary>
		/// Rental-Booking Service constructor.
		/// </summary>
		/// <param name="bookingService">Injected Booking service </param>
		/// <param name="rentalService">Injected Rental service </param>
		public RentalBookingService(IBookingService bookingService,
								  IRentalService rentalService)
		{
			this.bookingService = bookingService;
			this.rentalService = rentalService;
		}

		public async Task<Booking> AddBooking(Booking bookingToAdd)
		{
			var rental = rentalService.GetById(bookingToAdd.RentalId);
			if(rental == null) throw new HttpException(HttpStatusCode.NotFound, "Rental not found");
			return await bookingService.AddAsync(bookingToAdd, rental.Units, rental.PreparationTimeInDays);
		}

		public Rental UpdateRental(Rental newRental)
		{
			var rentalToUpdate = rentalService.GetById(newRental.Id);

			if (rentalToUpdate == null) throw new HttpException(HttpStatusCode.NotFound,"Rental not found");

			if (newRental.Units < rentalToUpdate.Units)
			{
				for (int unit = newRental.Units + 1; unit <= rentalToUpdate.Units; unit++)
				{
					if (bookingService.IsBookedByRentalUnitAndDate(rentalToUpdate.Id, unit, DateTime.Now.Date))
					{
						throw new HttpException(HttpStatusCode.Conflict, $"Unit {unit} cannot be removed because it is already booked.");
					}
				}
			}

			var preparationTimes = bookingService.GetPreparationTimesByRentalAndDate(newRental.Id, DateTime.Now.Date).ToList();

			foreach (var preparationTime in preparationTimes)
			{
				var overlappedBookings = bookingService.GetBookingsByDateRangeAndUnit(newRental.Id, preparationTime.Unit, preparationTime.Start, newRental.PreparationTimeInDays);

				overlappedBookings = overlappedBookings.Where(booking => booking.Id != preparationTime.Id);

				if (overlappedBookings.Count() > 0)
				{
					throw new HttpException(HttpStatusCode.Conflict, $"There would be an overlapping between preparation time {preparationTime.Id} and booking {string.Join(", ", overlappedBookings.Select(b => b.Id))}");
				}
			}
			if (newRental.PreparationTimeInDays != rentalToUpdate.PreparationTimeInDays)
			{
				bookingService.UpdatePreparationTimes(preparationTimes, newRental.PreparationTimeInDays);
			}

			newRental.Id = newRental.Id;
			rentalService.Update(newRental);
			return newRental;
		}
	}
}
