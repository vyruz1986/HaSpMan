using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Web.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var oidcConfig = configuration.GetSection(OidcConfig.SectionName).Get<OidcConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = oidcConfig.Authority;
                options.ClientId = oidcConfig.Audience;
                options.ClientSecret = oidcConfig.ClientSecret;
                options.ResponseType = Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectResponseType.Code;
                options.CallbackPath = new PathString("/callback");
                options.SaveTokens = true;
                options.Resource = oidcConfig.Audience;
                options.RequireHttpsMetadata = false; //TODO
                options.SaveTokens = true;

                // Add handling of lo
                options.Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                    {
                        var logoutUri = $"{oidcConfig.Authority}/protocol/openid-connect/logout";

                        var postLogoutUri = context.Properties.RedirectUri;
                        if (!string.IsNullOrEmpty(postLogoutUri))
                        {
                            if (postLogoutUri.StartsWith("/"))
                            {
                                var request = context.Request;
                                postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                            }
                            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                        }

                        context.Response.Redirect(logoutUri);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}