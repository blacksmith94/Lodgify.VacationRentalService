using Lodgify.VacationRentalService.Domain.Models.Interfaces;

namespace Lodgify.VacationRentalService.WebAPI.DTOs
{
	public class PreparationTimeResponse: IBookingPeriod
	{
		public int Unit { get; set; }
	}
}
