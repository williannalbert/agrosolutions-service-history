using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AgroSolutions.History.API.Configuration;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var authorityUrl = jwtSettings["Authority"];
        var audience = jwtSettings["Audience"];

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authorityUrl;
                options.Audience = audience;
                options.RequireHttpsMetadata = false;

                options.MetadataAddress = $"{authorityUrl}/.well-known/openid-configuration";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    ValidateIssuer = true,
                    ValidIssuers = new[]
                    {
                        authorityUrl,
                        "http://localhost:8080/realms/agrosolutions",
                        "http://keycloak-service.agrosolutions-identity:8080/realms/agrosolutions"
                    },

                    ValidateAudience = true,
                    ValidAudiences = new[] { audience },

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddAuthorization();

        return services;
    }
}