using System.Collections.Generic;

namespace Lodgify.VacationRentalService.Domain.Models
{
    public class Calendar
    {
        public int RentalId { get; set; }

        public List<CalendarDate> Dates { get; set; }
    }
}
