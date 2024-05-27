using Kampus.Data;
using Kampus.Data.Abstract;
using Kampus.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kampus.Api.Extensions
{
    public static class WebBuilderExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, 
            IConfiguration configuration)
        {
            string KampusDB = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<KampusContext>(options =>
                options.UseNpgsql(KampusDB, b => b.MigrationsAssembly("Kampus.Api")));

            services.AddScoped<IProfesorRepository, ProfessorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
