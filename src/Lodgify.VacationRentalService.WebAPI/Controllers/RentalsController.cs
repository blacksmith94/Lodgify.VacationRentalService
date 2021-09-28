using AutoMapper;
using Lodgify.VacationRentalService.Domain.Models;
using Lodgify.VacationRentalService.Domain.Services.Interfaces;
using Lodgify.VacationRentalService.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Lodgify.VacationRentalService.Api.Controllers
{
	[Route("api/v1/rentals")]
	[ApiController]
	public class RentalsController : ControllerBase
	{
		private readonly IRentalService rentalService;
		private readonly IRentalBookingService rentalBookingService;

		private readonly IMapper mapper;
		private readonly ILogger<RentalsController> logger;



		public RentalsController(IRentalService rentalService,
								 IRentalBookingService rentalBookingService,
								 IMapper mapper,
								 ILogger<RentalsController> logger)
		{
			this.rentalService = rentalService;
			this.mapper = mapper;
			this.logger = logger;
			this.rentalBookingService = rentalBookingService;
		}

		[HttpGet("{rentalId:int}")]
		[ProducesResponseType(typeof(RentalResponseDTO), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult<RentalResponseDTO> Get(int rentalId)
		{
			var rental = rentalService.GetById(rentalId);
			if (rental == null)
			{
				return NotFound("Rental not found");
			}

			logger.LogInformation($"GET rental '{rental.Id}'");

			return Ok(rental);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ResourceIdDTO), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<ResourceIdDTO>> Post(RentalRequestDTO rentalRequest)
		{
			//Map model
			var rental = mapper.Map<RentalRequestDTO, Rental>(rentalRequest);

			//Add
			var model = await rentalService.AddAsync(rental);
			if (model == null)
			{
				return BadRequest();
			}
			var response = mapper.Map<Rental, ResourceIdDTO>(model);

			logger.LogInformation($"POST rental '{response.Id}'");

			return Ok(response);
		}

		[HttpPut("{rentalId:int}")]
		[ProducesResponseType(typeof(ResourceIdDTO), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.Conflict)]
		public ActionResult<ResourceIdDTO> Put(int rentalId, [FromBody] RentalRequestDTO rentalRequest)
		{
			//Map model
			var model = mapper.Map<RentalRequestDTO, Rental>(rentalRequest);
			model.Id = rentalId;

			//Update
			var response = rentalBookingService.UpdateRental(model);

			if (response == null)
			{
				return BadRequest();
			}

			logger.LogInformation($"PUT rental '{response.Id}'");

			return Ok(response);
		}
	}
}
