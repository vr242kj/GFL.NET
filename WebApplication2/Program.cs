using System.Text;
using WebApplication2.Repositories;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddScoped<ApplicationDbContext>(provider => new ApplicationDbContext(builder.Configuration));
builder.Services.AddScoped<JsonRepository>();
builder.Services.AddScoped<JsonService>();

// Other service configurations
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.OutputEncoding = Encoding.UTF8;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Json}/{action=Index}");

app.Run();
