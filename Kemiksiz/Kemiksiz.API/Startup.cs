using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Kemiksiz.API.Infrastructure;
using Kemiksiz.DB;
using Kemiksiz.Service;
using Kemiksiz.Service.Bill;
using Kemiksiz.Service.Card;
using Kemiksiz.Service.Job;
using Kemiksiz.Service.Jwt;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemiksiz.API
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

            services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage());

            services.AddHangfireServer();

            services.Configure<CardDbConfig>(Configuration);

            // Mapper tanýmlandý
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();

            services.AddSingleton(mapper);

            // Servisler tanýmlandý
            services.AddTransient<IDbClient, DbClient>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IJobService, JobService>();

            // Redis Cache tanýmlandý
            services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kemiksiz.API", Version = "v1" });
            });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
             IBackgroundJobClient backgroundJobClient,
             IRecurringJobManager recurringJobManager,
             IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kemiksiz.API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire Job!"));
            recurringJobManager.AddOrUpdate("EmailOperation",
                () => serviceProvider.GetService<IJobService>().sendWelcomeEmail(),
                "* * * * *");
        }
    }
}
