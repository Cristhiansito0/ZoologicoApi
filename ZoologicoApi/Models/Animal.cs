namespace ZoologicoApi.Models
{
    public class Animal
    {
        // Clave primaria
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Genero { get; set; } = string.Empty; // Ej: 'M' o 'F'

        // --- Claves Foráneas (Foreign Keys) ---
        // Necesitamos estas propiedades para EF Core sepa cómo conectarlas a las tablas.
        public int RazaId { get; set; }
        public int EspecieId { get; set; }

        // --- Propiedades de Navegación (Para acceder al objeto completo) ---
        public Raza? Raza { get; set; } // Relación con la tabla Razas
        public Especie? Especie { get; set; } // Relación con la tabla Especies
    }
}
