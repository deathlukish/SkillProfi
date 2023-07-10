using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkillProfiApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("SkillProfiConnectionString");
var identityCon = builder.Configuration.GetConnectionString("IdentityConnectionString");
builder.Services.AddControllers();
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(identityCon));
builder.Services.AddIdentityCore<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "Server",
        ValidateAudience = true,
        ValidAudience = "Client",
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superpupersecurity")),
        ValidateIssuerSigningKey = true,
    };
});
var app = builder.Build();
SeedData.CreateMigrationBase(app);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
