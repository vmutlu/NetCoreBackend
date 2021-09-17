using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.DependencyResolvers;
using Apsiyon.Extensions;
using Apsiyon.Utilities.IoC;
using Apsiyon.Utilities.Security.Encryption;
using Apsiyon.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Apsiyon.API.Extensions
{
    public static class StartupConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApsiyonContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), option => { option.MigrationsAssembly("Apsiyon.DataAccess"); }));
        }

        public static void ConfigureDependecies(this IServiceCollection services, TokenOptions tokenOption)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOption.Issuer,
                        ValidAudience = tokenOption.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOption.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
        }
    }
}
