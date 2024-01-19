using BigDSignClientDesktop.Service;
using BigDSignWebApp.Cookies;
using BigDSignWebApp.Utilities;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// SignalR Service
builder.Services.AddSignalR();


// Add services to the container.

builder.Services.AddControllersWithViews();

// Add cookie authentication service and configure the cookie options
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(0.5);
        options.SlidingExpiration = true;
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Forbidden";
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(0.5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add dependency injections for web app layer
builder.Services.AddSingleton<ICookieProvider, CookieProvider>();

// Add dependency injections for business logic layer
builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<IBookingLogic, BookingLogic>();
builder.Services.AddSingleton<IBookingLineLogic, BookingLineLogic>();
builder.Services.AddSingleton<IEventLogic, EventLogic>();

// Add dependency injections for service layer
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<IBookingLineService, BookingLineService>();
builder.Services.AddSingleton<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
// Strict cookie policy for protecting against attacks, such as cross-site request forgery (CSRF)
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Map SignalR Hub
    endpoints.MapHub<EventHub>("/eventHub");
});

app.Run();
