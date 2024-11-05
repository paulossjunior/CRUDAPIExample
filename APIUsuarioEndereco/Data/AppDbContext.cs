using APIUsuarioEndereco.Model;
using Microsoft.EntityFrameworkCore;

namespace APIUsuarioEndereco.Data;

public class AppDbContext:DbContext
{
    private readonly DbContextOptions<AppDbContext> _options;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        _options = options;
    }

    public System.Data.Entity.DbSet<Usuario> Usuarios { get; set; }
    public System.Data.Entity.DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
                
            entity.HasMany(e => e.Enderecos)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Logradouro).IsRequired();
            entity.Property(e => e.Numero).IsRequired();
            entity.Property(e => e.Bairro).IsRequired();
            entity.Property(e => e.Cidade).IsRequired();
            entity.Property(e => e.Estado).IsRequired();
            entity.Property(e => e.CEP).IsRequired();
        });
    }

    
}