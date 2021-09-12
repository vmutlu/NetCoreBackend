using Apsiyon.API.Extensions;
using Apsiyon.CrossCuttingConcerns.Caching;
using Apsiyon.CrossCuttingConcerns.Caching.Microsoft;
using Apsiyon.DataAccess.Concrete.EntityFramework.Context;
using Apsiyon.DependencyResolvers;
using Apsiyon.Extensions;
using Apsiyon.Services.Abstract;
using Apsiyon.Services.Concrete;
using Apsiyon.Utilities.IoC;
using Apsiyon.Utilities.Security.Encryption;
using Apsiyon.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Apsiyon.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        private const string CorsPolicy = "AllowOrigin";

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration);

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.ConfigureDependecies(tokenOptions);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Apsiyon.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apsiyon.API v1"));
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
