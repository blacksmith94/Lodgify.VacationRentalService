using Lodgify.VacationRentalService.Domain.Models;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services.Interfaces
{
	public interface IRentalBookingService
	{
		public Task<Booking> AddBooking(Booking bookingToAdd);

		public Rental UpdateRental(Rental newRental);
	}
}
