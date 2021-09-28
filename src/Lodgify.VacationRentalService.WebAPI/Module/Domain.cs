using Autofac;
using Lodgify.VacationRentalService.Domain.Services;
using Lodgify.VacationRentalService.Domain.Services.Interfaces;

namespace Lodgify.VacationRentalService.WebAPI.Module
{
	/// <summary>
	/// This class adds the contents of the Domain module into the autofac IoC container.
	/// It registers the domain services.
	/// </summary>
	public class Domain : Autofac.Module
	{
		/// <summary>
		/// Register the domain services
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<BookingService>().As<IBookingService>();
			builder.RegisterType<RentalService>().As<IRentalService>();
			builder.RegisterType<CalendarService>().As<ICalendarService>();
			builder.RegisterType<RentalBookingService>().As<IRentalBookingService>();
		}
	}
}
