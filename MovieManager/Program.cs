using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;

//Builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Add connection string
var connectionString = builder.Configuration.GetConnectionString("DefauktConnection");
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Add DB and Identity Services
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.ConnectionString));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true);

builder.Services.AddControllersWithViews();

//Build
WebApplication app = builder.Build();

//Http request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage().UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error").UseHsts();
}

app.UseHttpsRedirection().UseStaticFiles().UseRouting()
    .UseAuthentication().UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");


//Run the App
app.Run();


//TestDbPlaylist.FillPlaylist();
//UserOperations.CreateUser();
//DbDebugMethods.FillMovies();