using AutoMapper;
using FluentValidation;
using Lodgify.VacationRentalService.Domain.Models;
using Lodgify.VacationRentalService.Domain.Services;
using Lodgify.VacationRentalService.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Api.Controllers
{
	[Route("api/v1/bookings")]
	[ApiController]
	public class BookingsController : ControllerBase
	{
		private readonly IBookingService bookingService;
		private readonly IRentalBookingService rentalBookingService;

		private readonly IValidator<BookingRequestDTO> bookingRequestValidator;
		private readonly IMapper mapper;
		private readonly ILogger<BookingsController> logger;

		public BookingsController(IBookingService bookingService,
								  IRentalBookingService rentalBookingService,
								  IValidator<BookingRequestDTO> bookingRequestValidator,
								  IMapper mapper,
								  ILogger<BookingsController> logger)
		{
			this.bookingService = bookingService;
			this.rentalBookingService = rentalBookingService;
			this.bookingRequestValidator = bookingRequestValidator;
			this.mapper = mapper;
			this.logger = logger;
		}

		[HttpGet("{bookingId:int}")]
		[ProducesResponseType(typeof(BookingResponseDTO), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult<BookingResponseDTO> Get(int bookingId)
		{
			var model = bookingService.GetById(bookingId);
			if (model== null)
			{
				return NotFound("Booking not found");
			}
			var response = mapper.Map<Booking, BookingResponseDTO>(model);

			logger.LogInformation($"GET booking '{model.Id}'");

			return Ok(response);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ResourceIdDTO), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<ActionResult<ResourceIdDTO>> Post(BookingRequestDTO bookingRequestDTO)
		{
			//Validate Request
			var requestValidation = bookingRequestValidator.Validate(bookingRequestDTO);
			if (!requestValidation.IsValid)
				return BadRequest(requestValidation);

			//Map model
			var model = mapper.Map<BookingRequestDTO, Booking>(bookingRequestDTO);

			//Add to db
			var addedBooking = await rentalBookingService.AddBooking(model);

			var response = mapper.Map<Booking, ResourceIdDTO>(addedBooking);

			logger.LogInformation($"POST booking '{response.Id}'");

			return Ok(response);
		}
	}
}
