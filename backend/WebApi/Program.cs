
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
            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false).Build();
            builder.Services.AddSingleton<ApplicationContext> (serviceProvider =>
                {
                    var settings = config.GetSection(nameof(DbSettings)).Get<DbSettings>();
                    return new ApplicationContext(settings.ConnectionString);
                });
            builder.Services.AddSingleton<ITaskRepository, DbTaskRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}