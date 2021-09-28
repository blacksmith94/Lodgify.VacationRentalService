using Lodgify.VacationRentalService.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Lodgify.VacationRentalService.Domain.Models
{
    public class CalendarDate
    {
        public DateTime Date { get; set; }

        public List<IBookingPeriod> Bookings { get; set; }

        public List<IBookingPeriod> PreparationTimes { get; set; }
    }
}
