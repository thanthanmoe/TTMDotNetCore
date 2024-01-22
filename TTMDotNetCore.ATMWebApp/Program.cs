using TTMDotNetCore.ATMWebApp.AppDB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using log4net.Config;
using log4net;

try
{
    var log4NetRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(log4NetRepository, new FileInfo("log4net.config"));
//XmlConfigurator.Configure(log4NetRepository, new FileInfo("log4nettxt.config"));
ILog log = LogManager.GetLogger(typeof(Program));
log.Info("This is a test message.");

var builder = WebApplication.CreateBuilder(args);
//for db
builder.Logging.AddLog4Net();


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseSqlServer(connectionString);
    }
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("An unexpected error occurred.");
        });
    });

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseSession();
app.Run();
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred during application startup: {e.Message}");
}

