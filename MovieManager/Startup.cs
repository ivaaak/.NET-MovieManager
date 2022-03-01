using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DBConfig;
using static MovieManager.Data.DataConstants;

namespace MovieManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false; 
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddUserStore<MovieContext>();

            //reigster context
            services.AddDbContext<MovieContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(Data.DBConfig.Configuration.ConnectionString));
            });


            services.AddMemoryCache();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
            
            //services.AddTransient<IService, Service>();
            //services.AddTransient<IStatisticsService, StatisticsService>();
        }

        //builder and endpoints
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    //endpoints.MapDefaultAreaRoute();

                    endpoints.MapControllerRoute(
                        name: "Movie Details",
                        pattern: "/Movie/Details/{id}/{information}",
                        defaults: new
                        {
                            //controller = typeof(MovieController).GetControllerName(), //add GetControllerName() method
                            //action = nameof(MovieController.Details)
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
