using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Helpers;
using IslamicSchool.Interfaces;
using IslamicSchool.Repository;
using IslamicSchool.UOW;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IslamicSchool.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseIISIntegration();
        });

        public static void ApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DbConnectionString"));
            });
            services.AddIdentityCore<AppUser>()
                    .AddRoles<AppRole>()
                    .AddRoleManager<RoleManager<AppRole>>()
                    .AddSignInManager<SignInManager<AppUser>>()
                    .AddRoleValidator<RoleValidator<AppRole>>()
                    .AddEntityFrameworkStores<DataContext>();
            var configMap = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapingProfile());
                cfg.CreateMap<Branch, GetBranchDto>();
                cfg.CreateMap<BranchDto, Branch>();
            });
            var mapper = configMap.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
