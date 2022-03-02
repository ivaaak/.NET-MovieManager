using Microsoft.EntityFrameworkCore;
using MovieManager;
using MovieManager.Data.DBConfig;
using MovieManager.Services;
using System.Collections.Generic;


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

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}"); 

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

//Run the App
app.Run();




//TestDbPlaylist.FillPlaylist();
//UserOperations.CreateUser();
//DbDebugMethods.FillMovies();