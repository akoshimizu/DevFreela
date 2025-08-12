using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure
{
    public static class InfraStructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddRepositories()
                .AddData(config);
            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
        {

            var connectionString = config.GetConnectionString("DevFreelaCs");

            services.AddDbContext<DevFreelaDbContext>(opt => opt.UseSqlServer(connectionString));
            
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
