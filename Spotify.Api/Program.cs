using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Spotify.Api.Validators;
using Spotify.Database;
using Spotify.Domain.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration["DefaultConnection"]));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
}).
AddEntityFrameworkStores<AppDbContext>().
AddDefaultTokenProviders();

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).
    AddJwtBearer(config =>
    {
        var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Secret"]);
        var key = new SymmetricSecurityKey(bytes);

        config.SaveToken = true;
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidIssuer = builder.Configuration["AuthIssuer"],
            ValidAudience = builder.Configuration["AuthAudience"],
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));

    options.AddPolicy("Manager", policy =>
        policy.RequireAssertion(context =>
        context.User.HasClaim("Role", "Manager")
        || context.User.HasClaim("Role", "Admin")));
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterModelValidator>();

builder.Services.AddApplicationInfrastucture();
builder.Services.AddApplicationServices();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Spotify API",
        Description = "API trying to mirror Spotify functions"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify API");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
