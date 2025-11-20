using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace ZoologicoApi.Data
{
    public class ZoologicoContextFactory
    {
       
            public ZoologicoContext CreateDbContext(string[] args)
            {
                // Opcional: Esto ayuda a cargar el appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                var builder = new DbContextOptionsBuilder<ZoologicoContext>();

                // Reemplaza con tu cadena de conexión o usa la línea de arriba
                // Asegúrate de que esta cadena de conexión esté CORRECTA
                // Ejemplo de cadena de conexión con SQL Server localdb:
                // "Server=(localdb)\\mssqllocaldb;Database=ZoologicoDB;Trusted_Connection=True;MultipleActiveResultSets=true"
                builder.UseSqlServer(connectionString);

                return new ZoologicoContext(builder.Options);
            }
        }
}
