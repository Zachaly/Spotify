using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotify.Database;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;
using System.Security.Claims;
using FluentValidation;
using Spotify.UI.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration["DefaultConnection"]));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Accounts/Login";
});

builder.Services.AddRazorPages();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
}).AddFluentValidation();

builder.Services.AddTransient<IApplicationUserManager, ApplicationUserManager>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterViewModelValidator>();

var app = builder.Build();

try
{
    // Creating default identities if they are not present
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            var admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@email.com"
            };

            userManager.CreateAsync(admin, "zaq1@WSX").GetAwaiter().GetResult();

            var adminClaim = new Claim("Role", "Admin");

            userManager.AddClaimAsync(admin, adminClaim).GetAwaiter().GetResult();
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseMvcWithDefaultRoute();

app.Run();
