using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentalOfPremises.Services;

namespace RentalOfPremises.Api.Infrastructure
{
    public static class AddAuthorizationExtensions
    {
        public static void GetAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = Authorazation.ISSUER,
                     ValidAudience = Authorazation.AUDIENCE,
                     IssuerSigningKey = Authorazation.GetSymmetricSecurityKey(),
                 };
             });
        }
    }
}
