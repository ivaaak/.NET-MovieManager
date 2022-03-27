using MovieManager.Infrastructure;

//Builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


//Add DbContext
builder.Services.AddApplicationDbContexts();

//Add DB and Identity Services
builder.Services.AddIdentityContext();

//Custom Services
builder.Services.AddApplicationServices();

//Build
WebApplication app = builder.Build();



//Http request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage().UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error").UseHsts();
}

app.UseHttpsRedirection().UseStaticFiles().UseRouting();
app.UseAuthentication().UseAuthorization();

app.MapControllerRoute(name: "Area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");


app.MapRazorPages(); //for Login/Register views

//Run the App
app.Run();
