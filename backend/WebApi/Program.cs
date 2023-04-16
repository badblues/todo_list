
using Microsoft.Extensions.Configuration;
using TodoList.Persistence;
using TodoList.Persistence.Configuration;
using TodoList.Persistence.Interfaces;

namespace WebApi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ApplicationContext> (serviceProvider =>
                {
                    var settings = config.GetSection(nameof(DbSettings)).Get<DbSettings>();
                    return new ApplicationContext(settings.ConnectionString);
                });
            builder.Services.AddSingleton<IConfiguration>(provider => config);
            builder.Services.AddSingleton<ITodoTaskRepository, DbTodoTaskRepository>();
            builder.Services.AddSingleton<IUserRepository, DbUserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}