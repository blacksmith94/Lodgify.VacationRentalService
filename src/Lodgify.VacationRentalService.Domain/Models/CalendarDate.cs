using System;
using System.Collections.Generic;

namespace Lodgify.VacationRentalService.Domain.Models
{
    public class CalendarDate
    {
        public DateTime Date { get; set; }

        public List<CalendarBooking> Bookings { get; set; }

        public List<CalendarBooking> PreparationTimes { get; set; }
    }
}
