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

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = jwtSettings["Authority"];
                options.Audience = jwtSettings["Audience"];
                options.RequireHttpsMetadata = false;

                options.MetadataAddress = $"{jwtSettings["Authority"]}/.well-known/openid-configuration";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuers = new[]
                    {
                        jwtSettings["Authority"],
                        jwtSettings["Authority"]?.Replace("keycloak:8080", "localhost:8080"),
                        "http://localhost:8080/realms/agrosolutions"
                    },
                    ValidateAudience = true,
                    ValidAudiences = new[] { jwtSettings["Audience"] },
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddAuthorization();

        return services;
    }
}