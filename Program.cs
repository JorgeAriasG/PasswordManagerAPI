using System.Xml.XPath;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using password_manager.api.Data;
using password_manager.api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --------------- Get the config file ---------------
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var settings = config.GetRequiredSection("Settings");

// --------------- Add services to the container ---------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PasswordManagerContext>(opt => opt.UseSqlServer(settings.GetConnectionString("PasswordManagerDB")));

// --------------- Add session middleware ---------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "PasswordManagerCookie";
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// --------------- Add interfaces for repository ---------------
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IPassword, PasswordRepository>();


// --------------- Build the application ---------------
var app = builder.Build();

// --------------- Configure the HTTP request pipeline ---------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSession();
app.Run();
