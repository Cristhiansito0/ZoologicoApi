namespace ZoologicoApi.Models;
    using System.Collections.Generic;

    public class Especie
    {
        // Clave primaria
        public int Id { get; set; }

        // Nombre de la especie (ej: "Perro", "Gato", "León")
        public string Nombre { get; set; } = string.Empty;

        // Relación: Una Especie puede tener muchos Animales.
        public ICollection<Animal>? Animales { get; set; }
    }
