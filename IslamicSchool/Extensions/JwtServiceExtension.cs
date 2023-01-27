using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IslamicSchool.Extensions
{
    public static class JwtServiceExtension
    {
        public static void JwtServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireDeanRole", policy => policy.RequireRole("DEAN"));
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("DEAN", "ADMIN"));
            });
        }
    }
}
