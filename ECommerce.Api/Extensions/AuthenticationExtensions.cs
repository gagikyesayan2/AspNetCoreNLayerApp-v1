
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using global::Ecommerce.Business.Config;

    namespace Ecommerce.Api.Extensions
    {
        public static class AuthenticationExtensions
        {
            public static IServiceCollection AddJwtAuthentication(
                this IServiceCollection services,
                IConfiguration configuration)
            {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            var jwt = configuration.GetSection("Jwt").Get<JwtSettings>()
                          ?? throw new InvalidOperationException("Jwt settings are missing");

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwt.Issuer,
                            ValidAudience = jwt.Audience,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                        };
                    });

                services.AddAuthorization();

                return services;
            }
        }
    }
