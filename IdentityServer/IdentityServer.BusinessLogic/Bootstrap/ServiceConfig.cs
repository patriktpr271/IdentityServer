using IdentityServer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityServer.BusinessLogic.Options;
using IdentityServer.BusinessLogic.Services.Interfaces.User;
using IdentityServer.BusinessLogic.Services.User;
using IdentityServer.BusinessLogic.Services.Interfaces;
using IdentityServer.Entities.Dto.Validators;
using IdentityServer.Entities.Dto.Validators.Interfaces;
using IdentityServer.Entities.Dtos;
using IdentityServer.Entities.Factories;


namespace IdentityServer.BusinessLogic.Bootstrap
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services);
            SetupAuthentication(services, configuration);
            return services;
        }
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            services.AddSingleton<ValidatorFactory>();
            services.AddSingleton<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            
            
            services.AddSingleton<PasswordHasher>()
                    .AddSingleton<ITokenProvider, TokenProvider>()
                    .AddScoped<IUserService, UserService>();    
            
                    
        }

        private static void SetupAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            
            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)), 
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                    };
                });
        }
    }
}
