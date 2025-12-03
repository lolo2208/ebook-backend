using Microsoft.Extensions.DependencyInjection;
using EbookBackend.Application.Services;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Application.Interfaces;
using EbookBackend.Application.Mappings;

namespace EbookBackend.Application.DependencyInjection
{
    public static class ApplicationServiceInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Servicio genérico
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

            //Servicios
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookReviewService, BookReviewService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISubGenreService, SubGenreService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();

            //AutoMapper
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            return services;
        }
    }
}
