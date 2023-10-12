using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddHaSpManContext(
       this IServiceCollection serviceCollection,
       string connectionString)
    {
        _ = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("The value cannot be empty or whitespace.", nameof(connectionString));
        }

        serviceCollection.AddDbContext<HaSpManContext>(
            options => options.UseSqlServer(connectionString, b => b
                        .MigrationsAssembly("Persistence")
                        .MigrationsHistoryTable("__EFMigrationsHistory", "HaSpMan")),
            ServiceLifetime.Transient);

        return serviceCollection;
    }

    public static void MigrateHaSpManContext(string connectionString)
    {
        _ = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("The value cannot be empty or whitespace.", nameof(connectionString));
        }
        var optionsBuilder = new DbContextOptionsBuilder<HaSpManContext>()
            .UseSqlServer(connectionString);

        var context = new HaSpManContext(optionsBuilder.Options);
        context.Database.Migrate();
    }
}
