using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StudioVeterinario.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        //Ogni tabella nel DB , ed ogni tabella che aggiungiamo nel db deve avere il Dbset

        public virtual DbSet<Animale> Animale { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipologiaAnimale> TipologiaAnimale { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Animale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipologiaAnimale>()
                .HasMany(e => e.Animale)
                .WithRequired(e => e.TipologiaAnimale)
                .WillCascadeOnDelete(false);
        }
    }
}
