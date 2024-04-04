using Microsoft.AspNetCore.Identity;
using Pustok.Core.Entities.Identity;
using Pustok.DataAccess.Contexts;

namespace Pustok.API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddDefaultTokenProviders()
                 .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
