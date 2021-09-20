using Lodgify.VacationRentalService.Domain.Models;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services
{
	public interface IRentalService
	{
		public Task<Rental> AddAsync(Rental rentalToAdd);

		public Rental GetById(int rentalId);

		public Rental Update(Rental rental);
	}
}