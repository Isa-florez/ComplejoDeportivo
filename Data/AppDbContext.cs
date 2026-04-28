using Microsoft.EntityFrameworkCore;
using ComplejoDeportivo.Models.Entities;

namespace ComplejoDeportivo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<EspacioDeportivo> EspaciosDeportivos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Reservas)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.EspacioDeportivo)
            .WithMany(e => e.Reservas)
            .HasForeignKey(r => r.EspacioDeportivoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Documento)
            .IsUnique();
        
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
    }
    
    
}

