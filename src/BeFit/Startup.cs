using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BeFit.Data;
using BeFit.Models;
using BeFit.Services;
using BeFit.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BeFit
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<BeFitDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AccountConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IMusclesRepository, MuscleRepository>();
            services.AddTransient<IGroupOfMusclesRepository, GroupOfMusclesRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IFillingWorkoutRepository, FillingWorkoutRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IAppUserRepository,AppUserRepository>();
            services.AddTransient<IMeasurementRepository, MeasurementRepository>();
            services.AddTransient<IFillMeasurementRepository, FillMeasurementRepository>();
            services.AddTransient<IWeightOfFoodRepository, WeightOfFoodRepository>();
            services.AddTransient<IIngestionRepository, IngestionRepository>();
            services.AddTransient<IOneDayFoodRepository, OneDayFoodRepository>();
            services.AddTransient<IOneDayWorkoutRepository, OneDayWorkoutRepository>();
            services.AddTransient<ICardioRepository, CardioRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
        }
  
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BeFitDbContext context, IServiceProvider serviceProvider)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();
           

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DbInitializer.Initialize(context);
            CreateRoles(serviceProvider).GetAwaiter();



        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //d create an admin who will maintain the web app
            var adminUser = new ApplicationUser
            {
                Login = "Admin",
                UserName = "Admin",
                Email = "Admin@mail.ru",
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = "Admin741";
            var _user = await UserManager.FindByEmailAsync("Admin@mail.ru");

            if (_user == null)
            {
                var createAdminUser = await UserManager.CreateAsync(adminUser, userPWD);
                if (createAdminUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(adminUser, "Admin");

                }
            }
        }
    }
}
