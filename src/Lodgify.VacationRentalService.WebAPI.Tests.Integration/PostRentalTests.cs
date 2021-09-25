using Lodgify.VacationRentalService.WebAPI.DTOs;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Lodgify.VacationRentalService.WebAPI.Tests.Integration
{
	[Collection("Integration")]
	public class PostRentalTests
	{
		private readonly Request _request;

		public PostRentalTests(IntegrationFixture fixture)
		{
            _request = fixture.Request;
		}

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new RentalRequestDTO
            {
                Units = 25,
                PreparationTimeInDays = 1
            };

            ResourceIdDTO resourceIdResponse = await _request.Post<RentalRequestDTO, ResourceIdDTO>("rentals", request);
            RentalResponseDTO rentalResponse = await _request.Get<RentalResponseDTO>($"rentals/{resourceIdResponse.Id}");
            Assert.Equal(request.Units, rentalResponse.Units);
            Assert.Equal(request.PreparationTimeInDays, rentalResponse.PreparationTimeInDays);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Negative_Units()
        {
            var request = new RentalRequestDTO
            {
                Units = -1,
                PreparationTimeInDays = 1
            };

            var response = await _request.Post("rentals", request);
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}
