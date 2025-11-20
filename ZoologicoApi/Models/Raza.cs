namespace ZoologicoApi.Models;
using System.Collections.Generic;

    public class Raza
    {
    // Clave primaria. EF Core la reconocerá automáticamente.
    public int Id { get; set; }

    // Nombre de la raza (ej: "Labrador", "Siamés")
    public string Nombre { get; set; } = string.Empty;

    // Relación: Una Raza puede tener muchos Animales.
    public ICollection<Animal>? Animales { get; set; }
}

