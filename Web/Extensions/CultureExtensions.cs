using System.Globalization;

using Microsoft.AspNetCore.Localization;

namespace Web.Extensions;

public static class CultureExtensions
{
    public static IServiceCollection AddLocalizationAndConfiguration(this IServiceCollection services)
    {
        services.AddLocalization();
        return services;
    }

    public static IApplicationBuilder UseRequestLocalizationWithSupportedCultures(this IApplicationBuilder app)
    {
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures)
            .Where(cul => !string.IsNullOrEmpty(cul.Name))
            .ToArray();

        var localizationOptions = new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture("nl-BE"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
