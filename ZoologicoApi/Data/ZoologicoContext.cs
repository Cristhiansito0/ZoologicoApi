
using Microsoft.EntityFrameworkCore;
using ZoologicoApi.Models;
namespace ZoologicoApi.Data;
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


    public class ZoologicoContext : DbContext
    {
        public ZoologicoContext(DbContextOptions<ZoologicoContext> options)
            : base(options)
        {
        }

        // Define un DbSet por cada modelo que quieres mapear a una tabla
        public DbSet<Raza> Razas { get; set; } = default!;
        public DbSet<Especie> Especies { get; set; } = default!;
        public DbSet<Animal> Animales { get; set; } = default!;

    }

