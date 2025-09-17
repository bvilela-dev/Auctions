using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Contracts;
using MassTransit.Testing;
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
    public class AuctionBusTests(CustomWebAppFactory factory) : IAsyncLifetime
    {
        private readonly HttpClient _httpClient = factory.CreateClient();
        private readonly ITestHarness _testHarness = factory.Services.GetTestHarness();

        [Fact]
        public async Task CreateAuction_WithValidObject_ShouldPublishAuctionCreated()
        {
            // Arrange
            var auction = GetAuctionForCreate();
            _httpClient.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("bob"));

            // Act
            var response = await _httpClient.PostAsJsonAsync("api/auctions", auction);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(await _testHarness.Published.Any<AuctionCreated>());
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task DisposeAsync()
        {
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
            DbHelper.ReinitDbForTests(db);
            return Task.CompletedTask;
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

    }
}
