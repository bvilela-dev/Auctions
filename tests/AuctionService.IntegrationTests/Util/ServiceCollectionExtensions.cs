using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests.Util
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveDbContext<T>(this IServiceCollection svc)
        {
            var descriptor = svc.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AuctionDbContext>));
            if (descriptor != null) svc.Remove(descriptor);

        }

        public static void EnsureCreated<T>(this IServiceCollection svc)
        {
            var sp = svc.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AuctionDbContext>();

            db.Database.Migrate();
            DbHelper.InitDbForTests(db);
        }
    }
}
