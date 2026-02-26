using KTMS.Infrastructure.Database;
using KTMS.Infrastructure.Database.Seeders;
using KTMS.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KTMS.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeDatabaseAsync(this IServiceProvider services, IHostEnvironment env)
        {
            await using var scope = services.CreateAsyncScope();
            var ctx = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            if (env.IsTest())
            {
                await ctx.Database.EnsureCreatedAsync();
                await DynamicDataSeeder.SeedAsync(ctx);
                return;
            }

            // SQL Server or similar
            await ctx.Database.MigrateAsync();//update-database

            if (env.IsDevelopment())
            {
                await DynamicDataSeeder.SeedAsync(ctx);
            }
        }
    }
}
