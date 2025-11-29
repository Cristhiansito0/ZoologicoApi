
namespace ZoologicoApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.EntityFrameworkCore;
using ZoologicoApi.Data;

public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);
        var baseDatosActiva = builder.Configuration.GetValue<string>("DatabaseProvider");
        builder.Services.AddDbContext<ZoologicoContext>(options =>
         { // 3. Usar un switch para seleccionar la conexión
             switch (baseDatosActiva)
             {
                 case "SqlServer":
                     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                     break;
                 case "MariaDB":
                     var connectionString = builder.Configuration.GetConnectionString("ZoologicoAPIConnection.mariadb");
                     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                     break;
                 case "Postgres":
                     options.UseNpgsql(builder.Configuration.GetConnectionString("ZoologicoAPIConnection.postgres"));
                     break;
                 case "Oracle":
                     options.UseOracle(builder.Configuration.GetConnectionString("ZoologicoAPIConnection.oracle"));
                     break;
                 default:
                   
                     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                     break;
             }
         });

        // Add services to the container.

        builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
