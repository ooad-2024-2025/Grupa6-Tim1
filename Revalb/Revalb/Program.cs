using Microsoft.EntityFrameworkCore;
using Revalb.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Dodavanje konekcije prema SQL Server bazi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ✅ Ako u budućnosti želiš dodati autentifikaciju ručno
// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

// ✅ Dodavanje MVC podrške
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔧 Middleware pipeline konfiguracija
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // prikazuje detalje o greškama u migracijama
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔴 Uklonjeno: app.UseAuthentication(); (jer ne koristiš Identity)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🔴 Uklonjeno: app.MapRazorPages(); (vezano za Identity UI)

app.Run();