using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests
{
    [Collection("Shared collection")]
    public class AuctionControllerTests(CustomWebAppFactory factory) : IAsyncLifetime
    {
        private readonly HttpClient _httpClient = factory.CreateClient();
        private const string _gT_ID = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

        [Fact]
        public async Task GetAuctions_ShouldReturn3Auctions()
        {
            // Arrange?


            // Act
            var response = await _httpClient.GetFromJsonAsync<List<AuctionDto>>("api/auctions");

            // Assert
            Assert.Equal(3, response.Count);
        }

        [Fact]
        public async Task GetAuctionById_WithValidId_ShouldReturnAuction()
        {
            // Act
            var response = await _httpClient.GetFromJsonAsync<AuctionDto>($"api/auctions/{_gT_ID}");

            // Assert
            Assert.Equal("GT", response?.Model);
        }

        [Fact]
        public async Task GetAuctionById_WithInvalidId_ShouldReturn404()
        {
            // Act
            var response = await _httpClient.GetAsync($"api/auctions/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetAuctionById_WithInvalidGuid_ShouldReturn400()
        {
            // arrange

            // act
            var response = await _httpClient.GetAsync($"api/auctions/notaguid");

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateAuction_WithNoAuth_ShouldReturn401()
        {
            // arrange
            var auction = new CreateAuctionDto { Manufacturer = "test" };

            // act
            var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

            // assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreateAuction_WithAuth_ShouldReturn201()
        {
            // arrange
            var auction = GetAuctionForCreate();
            _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

            // act
            var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();
            Assert.Equal("bob", createdAuction?.Seller);
        }

        [Fact]
        public async Task CreateAuction_WithInvalidCreateAuctionDto_ShouldReturn400()
        {
            // arrange
            var auction = GetAuctionForCreate();
            auction.Manufacturer = null!;
            _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

            // act
            var response = await _httpClient.PostAsJsonAsync($"api/auctions", auction);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAuction_WithValidUpdateDtoAndUser_ShouldReturn200()
        {
            // arrange
            var updatedAuction = new UpdateAuctionDto { Manufacturer = "Updated" };
            _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

            // act
            var response = await _httpClient.PutAsJsonAsync($"api/auctions/{_gT_ID}", updatedAuction);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAuction_WithValidUpdateDtoAndInvalidUser_ShouldReturn403()
        {
            // arrange
            var updatedAuction = new UpdateAuctionDto { Manufacturer = "Updated" };
            _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("notbob"));

            // act
            var response = await _httpClient.PutAsJsonAsync($"api/auctions/{_gT_ID}", updatedAuction);

            // assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }


        private static CreateAuctionDto GetAuctionForCreate()
        {
            return new CreateAuctionDto
            {
                Manufacturer = "test",
                Model = "testModel",
                ImageUrl = "test",
                Description = "A test",
                Color = "test",
                Year = 10,
                ReservePrice = 10
            };
        }



        public Task InitializeAsync() => Task.CompletedTask;
     
        public Task DisposeAsync()
        {
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
            DbHelper.ReinitDbForTests(db);
            return Task.CompletedTask;
        }

    }
}
