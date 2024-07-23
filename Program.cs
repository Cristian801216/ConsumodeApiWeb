using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConsumodeApi.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ConsumodeApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConsumodeApiContext") ?? throw new InvalidOperationException("Connection string 'ConsumodeApiContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

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
    pattern: "{controller=Fincas}/{action=Index}/{id?}");

app.Run();
