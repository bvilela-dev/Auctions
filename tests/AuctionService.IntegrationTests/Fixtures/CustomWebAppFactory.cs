using AuctionService.Data;
using AuctionService.IntegrationTests.Util;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using WebMotions.Fake.Authentication.JwtBearer;

namespace AuctionService.IntegrationTests.Fixtures
{
    public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

        public async Task InitializeAsync()
        {
            await _postgreSqlContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(svc =>
            {
                svc.RemoveDbContext<AuctionDbContext>();

                svc.AddDbContext<AuctionDbContext>(opt =>
                {
                    opt.UseNpgsql(_postgreSqlContainer.GetConnectionString());
                });

                svc.AddMassTransitTestHarness();

                svc.EnsureCreated<AuctionDbContext>();

                svc.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                      .AddFakeJwtBearer(opt =>
                      {
                          opt.BearerValueType = FakeJwtBearerBearerValueType.Jwt;
                      });
            });
        }
        Task IAsyncLifetime.DisposeAsync() => _postgreSqlContainer.DisposeAsync().AsTask();
    }
}
