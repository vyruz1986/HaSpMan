using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HaSpMan.Persistence.Extensions
{
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

         serviceCollection.AddDbContext<HaSpManContext>(options =>
                     options.UseSqlServer(connectionString, b => b
                         .MigrationsAssembly("HaSpMan.Persistence")
                         .MigrationsHistoryTable("__EFMigrationsHistory", nameof(HaSpMan))));
         return serviceCollection;
      }

      public static void MigrateHaSpManContext(this IServiceCollection services, string connectionString)
      {
         var serviceProvider = services.BuildServiceProvider();
         var optionsBuilder = new DbContextOptionsBuilder<HaSpManContext>().UseSqlServer(connectionString, b => b
             .MigrationsAssembly("HaSpMan.Persistence")
             .MigrationsHistoryTable("__EFMigrationsHistory", nameof(HaSpMan)));

         var context = new HaSpManContext(optionsBuilder.Options);
         context.Database.Migrate();
      }
   }
}