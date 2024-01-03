using TTMDotNetCore.ATMWebApp.AppDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
	string? connectionString = builder.Configuration.GetConnectionString("DbConnection");
	options.UseSqlServer(connectionString);
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

// Add session support
builder.Services.AddDistributedMemoryCache(); // In-memory cache (for development)
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the session timeout as needed
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseSession(); // Move this line here to ensure the session middleware is executed in the correct order.

app.Run();
