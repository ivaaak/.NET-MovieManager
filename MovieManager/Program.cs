using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Services;
using MovieManager.Services.ServicesContracts;


//Builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Add connection string
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.ConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Add DB and Identity Services
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>().AddEntityFrameworkStores<MovieContext>();

//Custom Services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISearchMethodsService, SearchMethodsService>();
builder.Services.AddScoped<IAddToDbService, AddToDbService>();
builder.Services.AddScoped<IGetFromDbService, GetFromDbService>();
builder.Services.AddScoped<ISaveMovieToDbObjectService, SaveMovieToDbObjectService>();
builder.Services.AddScoped<IDeleteFromDbService, DeleteFromDbService>();
builder.Services.AddScoped<IApiGetListsService, ApiGetListsService>();

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

app.MapRazorPages(); //Login/Register views DONT load without this as they are RazorPages for some reason

//Run the App
app.Run();



//TestDbPlaylist.FillPlaylist();
//DbDebugMethods.FillMovies();