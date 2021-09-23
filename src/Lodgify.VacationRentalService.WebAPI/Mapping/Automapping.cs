using AutoMapper;
using Lodgify.VacationRentalService.Domain.Models;
using Lodgify.VacationRentalService.WebAPI.DTOs;

namespace Lodgify.VacationRentalService.WebAPI.Mapping
{
	/// <summary>
	/// This class defines the mapping between DTOs and models.
	/// <para/>
	/// Custom mapping can also be added by using .ConvertUsing<CustomConverter>() where CustomConverter would be a class that implements ITypeConverter;
	/// </summary>
	public class Automapping : Profile
	{
		public Automapping()
		{
			//Rental
			CreateMap<RentalRequestDTO, Rental>();
			CreateMap<Rental, ResourceIdDTO>();

			//Booking
			CreateMap<BookingRequestDTO, Booking>();
			CreateMap<Booking, ResourceIdDTO>();
			CreateMap<Booking, BookingResponseDTO>();

			//Calendar
			CreateMap<CalendarRequestDTO, CalendarRequest>();
			CreateMap<Calendar, CalendarResponse>();
			CreateMap<CalendarDate, CalendarDateResponse>();
			CreateMap<CalendarBooking, CalendarBookingResponse>();
			CreateMap<CalendarBooking, PreparationTimeResponse>();
		}
	}
}
