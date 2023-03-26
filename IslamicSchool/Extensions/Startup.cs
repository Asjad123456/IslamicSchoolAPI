using Microsoft.AspNetCore.Builder;

namespace IslamicSchool.Extensions
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.AuthenticationDisplayName = null;
            });
        }
    }
}
