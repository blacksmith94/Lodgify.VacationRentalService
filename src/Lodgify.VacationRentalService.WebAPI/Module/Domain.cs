using Autofac;
using FluentValidation;
using Lodgify.VacationRentalService.Domain.Services;
using Lodgify.VacationRentalService.WebAPI.DTOs;
using Lodgify.VacationRentalService.WebAPI.Validation;

namespace Lodgify.VacationRentalService.WebAPI.Module
{
	/// <summary>
	/// This class adds the contents of the Domain module into the autofac IoC container.
	/// It registers the domain services and validators.
	/// </summary>
	public class Domain : Autofac.Module
	{
		/// <summary>
		/// Register the domain services and validators
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<BookingService>().As<IBookingService>();
			builder.RegisterType<RentalService>().As<IRentalService>();
			builder.RegisterType<CalendarService>().As<ICalendarService>();
			builder.RegisterType<RentalBookingService>().As<IRentalBookingService>();

			builder.RegisterType<BookingRequestValidator>().As<IValidator<BookingRequestDTO>>();
			builder.RegisterType<CalendarRequestValidator>().As<IValidator<CalendarRequestDTO>>();
			builder.RegisterType<RentalRequestValidator>().As<IValidator<RentalRequestDTO>>();

		}
	}
}
