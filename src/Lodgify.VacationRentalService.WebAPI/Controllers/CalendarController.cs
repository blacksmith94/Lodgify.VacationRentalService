using AutoMapper;
using Lodgify.VacationRentalService.Domain.Models;
using Lodgify.VacationRentalService.Domain.Services;
using Lodgify.VacationRentalService.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Lodgify.VacationRentalService.Api.Controllers
{
	[Route("api/v1/calendar")]
	[ApiController]
	public class CalendarController : ControllerBase
	{
		private readonly ICalendarService calendarService;

		private readonly IMapper mapper;
		private readonly ILogger<CalendarController> logger;

		public CalendarController(ICalendarService calendarService,
								  IMapper mapper,
								  ILogger<CalendarController> logger)
		{
			this.calendarService = calendarService;
			this.mapper = mapper;
			this.logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(CalendarResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult<CalendarResponse> Get([FromQuery] CalendarRequestDTO calendarRequest)
		{
			//Map model
			var model = mapper.Map<CalendarRequestDTO, CalendarRequest>(calendarRequest);

			var calendar = calendarService.Get(model);

			var calendarResponse = mapper.Map<Calendar, CalendarResponse>(calendar);

			logger.LogInformation($"GET calendar with rental '{calendarResponse.RentalId}' and date '{calendarResponse.Dates}'");

			return Ok(calendarResponse);
		}
	}
}