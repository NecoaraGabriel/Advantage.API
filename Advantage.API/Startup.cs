using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AdvantageData.Models;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace AdvantageData.API
{
    public class Startup
    {
        private string _connectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connectionString = Configuration["psqlConnectionString"];
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy",
                    c => c.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            services.AddControllers();
            services.AddMvc();
            services.AddEntityFrameworkNpgsql().AddDbContext<ModelsDbContex>(
                    opt => opt.UseNpgsql(_connectionString)
                );
            services.AddTransient<DataSeed>(); //run only one
            services.AddControllersWithViews();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = Path.Combine(Directory.GetCurrentDirectory(), "../NgSight");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            seed.SeedData(30, 100);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}"
                    ); 
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../NgSight";
                //if (env.IsDevelopment())
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });

        }
    }
}
