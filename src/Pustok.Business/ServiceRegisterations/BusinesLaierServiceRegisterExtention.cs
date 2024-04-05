using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.HelperServices.Implementations;
using Pustok.Business.HelperServices.Interface;
using Pustok.Business.MappingProfiles;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.Business.Validators.AuthorValidators;

namespace Pustok.Business.ServiceRegisterations
{
    public static class BusinesLaierServiceRegisterExtention
    {
        public static IServiceCollection ServiceLayerServiceRegister(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(AuthorMapper).Assembly);
            services.AddAutoMapper(typeof(BookMapper).Assembly);

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddFluentValidation(o => o.RegisterValidatorsFromAssembly(typeof
                (AuthorPostDtoValidator).Assembly));


            return services;
        }

    }
}
