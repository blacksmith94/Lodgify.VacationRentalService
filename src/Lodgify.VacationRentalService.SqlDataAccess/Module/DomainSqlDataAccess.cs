using Autofac;
using Lodgify.VacationRentalService.Domain;
using Lodgify.VacationRentalService.Domain.Models;

namespace Lodgify.VacationRentalService.SqlDataAccess.Module
{
	/// <summary>
	/// This class adds the contents of the SqlDataAccess module into the autofac IoC container.
	/// It registers the EF Repositories.
	/// </summary>
	public class DomainSqlDataAccess : Autofac.Module
	{

		/// <summary>
		/// Overriden Load function to add registrations to the container.
		/// All the domain model repositories should be registered here.
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<EFRepository<Booking>>().As<IRepository<Booking>>();
			builder.RegisterType<EFRepository<Rental>>().As<IRepository<Rental>>();
		}
	}
}
