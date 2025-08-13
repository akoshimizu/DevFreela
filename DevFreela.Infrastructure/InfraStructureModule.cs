using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Autentication;
using DevFreela.Infrastructure.Notifications;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using System.Text;

namespace DevFreela.Infrastructure
{
    public static class InfraStructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddRepositories()
                .AddData(config)
                .AddAuth(config)
                .AddEmailService(config);


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
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthService>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Jwt:Key"]
                            ))
                    };
                });

            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(opt =>
            {
                opt.ApiKey = configuration.GetValue<string>("SendGrid:ApiKey");
            });

            return services;
        }
    }
}
