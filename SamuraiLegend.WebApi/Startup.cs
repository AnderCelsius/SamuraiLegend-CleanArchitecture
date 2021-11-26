using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SamuraiLegend.Application;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Infrastructure.Persistence;
using SamuraiLegend.Infrastructure.Persistence.Contexts;
using SamuraiLegend.Infrastructure.Persistence.Models;
using SamuraiLegend.Infrastructure.Persistence.Seeds;
using SamuraiLegend.Infrastructure.Shared;
using SamuraiLegend.WebApi.Extensions;
using SamuraiLegend.WebApi.Services;
using Serilog;

namespace SamuraiLegend.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddSharedInfrastructure(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.ConfigureIdentity(Configuration);
            services.AddSwaggerExtension();
            services.AddControllers()
                .AddNewtonsoftJson(op => op.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddSingleton(Log.Logger);
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            Seeder.SeedData(dbContext, userManager, roleManager).GetAwaiter().GetResult();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
