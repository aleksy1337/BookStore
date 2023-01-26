using BookStore1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStore1.Interfaces;
using BookStore1.Repository;
using BookStore1.Helpers;
using BookStore1.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower()== "seeddata")
{
    Seed.SeedData(app);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
