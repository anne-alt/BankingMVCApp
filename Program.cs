using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BankingMVCApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

var bankingAppConnectionString = builder.Configuration.GetConnectionString("BankingAppConnection") ?? throw new InvalidOperationException("Connection string 'BankingAppConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(bankingAppConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data function
static async Task SeedData(AppDbContext context)
{
    if (!await context.BankAccounts.AnyAsync())
    {
        var fakeAccounts = FakerBankAccounts.GenerateFakeBankAccounts(10);
        await context.BankAccounts.AddRangeAsync(fakeAccounts);
        await context.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseMigrationsEndPoint();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<AppDbContext>();

        await SeedData(dbContext); // Seed data
    }
}
else
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
app.MapRazorPages();

app.Run();
