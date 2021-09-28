using System;

namespace Lodgify.VacationRentalService.WebAPI.DTOs
{
	public class BookingResponseDTO
	{
		public int Id { get; set; }
		public int RentalId { get; set; }
		public DateTime Start { get; set; }
		public int Nights { get; set; }
		public int Unit { get; set; }
	}
}
