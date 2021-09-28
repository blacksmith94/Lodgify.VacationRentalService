using Lodgify.VacationRentalService.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Domain.Services
{
	/// <summary>
	/// This class will be registered as a service and will handle the domain's Rental logic
	/// </summary>
	public class RentalService : IRentalService
	{
		private readonly IRepository<Rental> repository;

		/// <summary>
		/// Rental Service constructor.
		/// </summary>
		/// <param name="repository">Injected the repository for the Rental table </param>
		public RentalService(IRepository<Rental> repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Task that stores a booking in DB.
		/// </summary>
		/// <param name="rentalToAdd">Rental model to add.</param>
		/// <returns>A Task with with a bool indicating if it was added or not</returns>

		public async Task<Rental> AddAsync(Rental rentalToAdd)
		{
			await repository.Add(rentalToAdd);
			await repository.Save();
			return rentalToAdd;
		}

		public Rental GetById(int rentalId)
		{
			var query = this.repository.Query;
			var rental = query.FirstOrDefault(b => b.Id == rentalId);
			return rental;
		}

		public Rental Update(Rental rental)
		{
			repository.Update(rental);
			repository.Save();
			return rental;
		}
	}
}
