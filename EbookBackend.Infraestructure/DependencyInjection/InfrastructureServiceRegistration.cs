using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EbookBackend.Infraestructure.Persistence;
using EbookBackend.Infraestructure.Security.Encryption;
using EbookBackend.Infraestructure.FileStorage;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Infraestructure.Persistence.Repositories;
using EbookBackend.Infraestructure.Security.Authentication;

namespace EbookBackend.Infraestructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var protector = new ConnectionStringProtector(config);
            var decryptedConn = protector.DecryptConnectionString(config["ConnectionStrings:EbookStoreFree"]!);

            //DBContext
            services.AddDbContext<EbookStoreDbContext>(options =>
                options.UseSqlServer(decryptedConn));

            //Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookReviewRepository, BookReviewRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISubGenreRepository, SubGenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();


            //Security
            services.AddSingleton<JwtTokenGenerator>();
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<ConnectionStringProtector>();

            //Blob Storage
            services.AddSingleton(provider =>
                new AzureBlobStorageService(config["Azure:BlobConnection"]!));

            return services;
        }
    }
}
