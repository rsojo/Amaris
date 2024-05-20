using Amaris.Helpers.Configs;
using Amaris.Services.Implementations;
using Amaris.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add APIUrl to the services.
builder.Services.Configure<ApiUrl>(builder.Configuration.GetSection("ApiUrl"));

// Add EmployeeService as an implementarion.
builder.Services.AddScoped<IEmployeeService, EmployeeService>(); ;

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
