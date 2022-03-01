using Microsoft.EntityFrameworkCore;
using MovieManagerMVC;
using MovieManagerMVC.Data.DBConfig;
using MovieManagerMVC.Services;

//AddTestManyToOne.AddMovieToPlaylist(398181, "08b11345-8f16-459c-ba97-aac8c630731f"); //current


//Builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieContext>
    (options => options.UseSqlServer
    (Configuration.ConnectionString));

//Settings from Startup
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder
    .UseStartup<Startup>());

//Build Web App
WebApplication app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Home}"); 

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

//Run the App
app.Run();