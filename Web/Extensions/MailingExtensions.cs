using Commands.Configuration;
using Commands.Services;

namespace Web.Extensions;

public static class MailingExtensions
{
    public static IServiceCollection AddMailingService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MailingConfiguration>()
            .Bind(configuration.GetSection(MailingConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IMailingService, MailingService>();

        return services;
    }
}
