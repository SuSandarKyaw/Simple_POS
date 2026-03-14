using _5BB_POS.Models;
using _5BB_POS.Repositories;
using _5BB_POS.Repositories.IRepository;
using _5BB_POS.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + "logs\\", "app_.log"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10485760,
        retainedFileCountLimit: 100
    )
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SimplePosContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//repo
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//service
builder.Services.AddScoped<CategoryService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
