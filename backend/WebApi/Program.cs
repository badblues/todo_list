
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoList.Persistence;
using TodoList.Persistence.Configuration;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
var dbSettings = builder.Configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();
if (dbSettings is null)
    return;
builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseNpgsql(dbSettings.ConnectionString);
    });
builder.Services.AddSingleton<IConfiguration>(provider => builder.Configuration);
builder.Services.AddScoped<ITodoTaskRepository, DbTodoTaskRepository>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var token = builder.Configuration.GetSection("AppSettings:Token").Value;
if (token is null)
    return;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
