using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

// must be placed before AddControllersWithViews
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    //Changes idle timeout to 5 minutes
    options.IdleTimeout = TimeSpan.FromSeconds(60 * 5);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = false;
    });


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro")));

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

//Must be added before routes are mapped
app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
