using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Projekat.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using Hangfire;
using Hangfire.SqlServer;

namespace Projekat
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
            services.AddDbContext<VinylContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=model;Trusted_Connection=True;");
            });
            services.AddControllers(options => options.EnableEndpointRouting=false)
            .AddNewtonsoftJson(options =>{
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.MaxDepth = 1;
            });
            services.AddMvc().AddJsonOptions(options =>{
                options.JsonSerializerOptions.WriteIndented=true;
                options.JsonSerializerOptions.PropertyNamingPolicy=null;
                options.JsonSerializerOptions.MaxDepth = 1;
            }).AddXmlSerializerFormatters();
            services.AddCors(options =>{
                options.AddPolicy("LocalhostAllow", builder =>{
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    //.WithOrigins
                    //(
                    //    "https://localhost:5001",
                    //    "http://localhost:8080"
                    //);
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
                options.TokenValidationParameters=new TokenValidationParameters{
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // ValidIssuer = "https://localhost:5001/",
                    // ValidAudience= "http://localhost:8080/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VinylAPISecretKey"))
                };
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Database=model;Trusted_Connection=True;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                    
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvc();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("LocalhostAllow");

            app.UseHangfireDashboard();
        }
    }
}
