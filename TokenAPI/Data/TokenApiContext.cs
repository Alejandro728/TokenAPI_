using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokenAPI.Data.TokenModels;

namespace TokenAPI.Data;

public partial class TokenApiContext : DbContext
{
    public TokenApiContext()
    {
    }

    public TokenApiContext(DbContextOptions<TokenApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83FE85EE619");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Identificador)
                .HasMaxLength(20)
                .HasColumnName("identificador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
