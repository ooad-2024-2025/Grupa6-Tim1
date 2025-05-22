using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Revalb.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Dodavanje konekcije prema SQL Server bazi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

string[] roles = { "Artist", "Recenzent" };

foreach (var role in roles)
{
    if (!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}//#143034
