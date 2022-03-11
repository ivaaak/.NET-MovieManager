using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Infrastructure.Extensions;

namespace MovieManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Db Context
            services
                .AddDbContext<MovieContext>(options => options
                    .UseSqlServer(Configuration
                    .GetConnectionString(Data.DBConfig.Configuration.ConnectionString)));

            services.AddDatabaseDeveloperPageExceptionFilter();


            //Identiry and Login options
            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedEmail = false;   
                    options.SignIn.RequireConfirmedAccount = false;   
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MovieContext>();

            services.AddMemoryCache();

            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            //});

    //TODO Register Services Here
            //services.AddTransient<ICarService, CarService>();
            //services.AddTransient<IDealerService, DealerService>();
            //services.AddTransient<IStatisticsService, StatisticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

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

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();

                    endpoints.MapControllerRoute(
                        name: "Car Details",
                        pattern: "/Cars/Details/{id}/{information}",
                        defaults: new
                        {
                            //controller = typeof(MovieController).GetControllerName(),
                            //action = nameof(MovieController.Details)
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
