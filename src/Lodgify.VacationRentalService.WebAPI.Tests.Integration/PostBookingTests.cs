using Lodgify.VacationRentalService.WebAPI.DTOs;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Lodgify.VacationRentalService.WebAPI.Tests.Integration
{
	[Collection("Integration")]
	public class PostBookingTests
	{
		private readonly Request _request;

		public PostBookingTests(IntegrationFixture fixture)
		{
			_request = fixture.Request;
		}

		[Fact]
		public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
		{
			//Post Rental
			var postRentalRequest = new RentalRequestDTO { Units = 4 };
			ResourceIdDTO postRentalResult = await _request.Post<RentalRequestDTO, ResourceIdDTO>("rentals", postRentalRequest);

			//Post Booking
			var postBookingRequest = new BookingRequestDTO
			{
				RentalId = postRentalResult.Id,
				Nights = 3,
				Start = new DateTime(2021, 09, 20)
			};
			ResourceIdDTO postBookingResult = await _request.Post<BookingRequestDTO, ResourceIdDTO>("bookings", postBookingRequest);

			//Get Booking
			var getBookingResponse = await _request.Get<BookingResponseDTO>($"bookings/{postBookingResult.Id}");
			Assert.Equal(postBookingRequest.RentalId, getBookingResponse.RentalId);
			Assert.Equal(postBookingRequest.Nights, getBookingResponse.Nights);
			Assert.Equal(postBookingRequest.Start, getBookingResponse.Start);
		}

		[Fact]
		public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
		{
			//Add Rental
			var postRentalRequest = new RentalRequestDTO { Units = 1, PreparationTimeInDays = 1 };
			ResourceIdDTO postRentalResult = await _request.Post<RentalRequestDTO, ResourceIdDTO>("rentals", postRentalRequest);

			//Add same Booking twice
			var postBookingRequest = new BookingRequestDTO { RentalId = postRentalResult.Id, Nights = 1, Start = new DateTime(2021, 09, 21) };
			var postBookingResponse = await _request.Post("bookings", postBookingRequest);
			Assert.True(postBookingResponse.IsSuccessStatusCode);

			postBookingResponse = await _request.Post("bookings", postBookingRequest);
			Assert.True(postBookingResponse.StatusCode == HttpStatusCode.BadRequest);
		}
	}
}
