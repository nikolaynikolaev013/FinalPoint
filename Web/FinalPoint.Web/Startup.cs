namespace FinalPoint.Web
{
    using System.IO;
    using System.Reflection;

    using FinalPoint.Data;
    using FinalPoint.Data.Common;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Data.Models;
    using FinalPoint.Data.Repositories;
    using FinalPoint.Data.Seeding;
    using FinalPoint.Services.Mapping;
    using FinalPoint.Services.Messaging;
    using FinalPoint.Web.Business.Interfaces;
    using FinalPoint.Web.Business.Services;
    using FinalPoint.Web.ViewModels;
    using FinalPoint.Web.ViewModels.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseLazyLoadingProxies()
                    .UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => false;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();

            services.Configure<MailSettings>(this.configuration.GetSection("MailSettings"));

            var mapper = AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            services.AddSingleton(mapper);
            services.AddSingleton(this.configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IParcelService, ParcelService>();
            services.AddTransient<IProtocolService, ProtocolService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IDbService, DbService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IThemeService, ThemeService>();
            services.AddTransient<IHttpFacade, HttpFacade>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = new PathString("/wwwroot"),
            });

            app.UseEndpoints(
                endpoints =>
                {
                        endpoints.MapControllerRoute(
                            "Add", "Add", new { controller = "AddDispose", action = "AddParcel" });

                        endpoints.MapControllerRoute(
                            "Dispose", "Dispose", new { controller = "AddDispose", action = "DisposeParcel" });

                        endpoints.MapControllerRoute(
                                    "Load", "Load", new { controller = "LoadUnload", action = "Load" });

                        endpoints.MapControllerRoute(
                                    "Load", "ProtocolApi/{protocolId?}", new { controller = "LoadUnload", action = "LoadProtocol" });

                        endpoints.MapControllerRoute(
                            "Unload", "Unload/{protocolId?}", new { controller = "LoadUnload", action = "Unload" });

                        endpoints.MapControllerRoute(
                                "CheckedParcelResult", "CheckedParcelResult", new { controller = "LoadUnload", action = "CheckedParcelResult" });

                        endpoints.MapControllerRoute(
                                    "ReloadParcelsTable", "ReloadParcelsTable", new { controller = "LoadUnload", action = "ReloadParcelsTable" });

                        endpoints.MapControllerRoute(
                            "Assets", "Assets", new { controller = "Home", action = "Assets" });

                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
