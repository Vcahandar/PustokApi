using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.MappingProfiles;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;


namespace Pustok.Business.ServiceRegisterations
{
    public static class BusinesLaierServiceRegisterExtention
    {
        public static IServiceCollection ServiceLayerServiceRegister(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(AuthorMapper).Assembly);

            services.AddScoped<IAuthorService, AuthorService>();


            return services;
        }

    }
}
