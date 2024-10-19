using Lamazon.Services.Extensions;
using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Lamazon.Web.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
builder.Services.InjectDbContext(connectionString);
builder.Services.InjectRepositories();
builder.Services.InjectService();
builder.Services.InjectAutoMapper();
builder.Services.InjectFluentValidators();
builder.Services.AddHttpClient<IGeoTrackerService, GeoTrackerService>();

builder.Services
    // Configures the app to use cookie authentication, where cookies will be used to identify the authenticated user.
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    // Configures cookie-specific settings:
    .AddCookie(options =>
        {
            // When a user tries to access a secured resource without being authenticated, they are redirected to the login page.
            options.LoginPath = "/Users/Login";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            // The cookie expiration time will reset if the user is active within the set expiration period, extending their session.
            options.SlidingExpiration = true;
        }
    );
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDebugIpAddressMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Administration",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );


    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});




app.Run();
