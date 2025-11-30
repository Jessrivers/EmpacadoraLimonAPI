using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmpacadoraLimonAPI.Models;

public partial class DbEmpacadoraContext : DbContext
{
    public DbEmpacadoraContext()
    {
    }

    public DbEmpacadoraContext(DbContextOptions<DbEmpacadoraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Destino> Destinos { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("proveedores");

            entity.HasKey(e => e.IdProveedor).HasName("proveedores_pkey");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(255);
            entity.Property(e => e.Latitud).HasColumnName("latitud").HasColumnType("decimal(10, 8)").IsRequired();
            entity.Property(e => e.Longitud).HasColumnName("longitud").HasColumnType("decimal(11, 8)").IsRequired();
            entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.ToTable("destinos");

            entity.HasKey(e => e.IdDestino).HasName("destinos_pkey");

            entity.Property(e => e.IdDestino).HasColumnName("id_destino");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(255);
            entity.Property(e => e.Latitud).HasColumnName("latitud").HasColumnType("decimal(10, 8)").IsRequired();
            entity.Property(e => e.Longitud).HasColumnName("longitud").HasColumnType("decimal(11, 8)").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.ToTable("lotes");

            entity.HasKey(e => e.IdLote).HasName("lotes_pkey");

            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor").IsRequired();
            entity.Property(e => e.FechaRecepcion).HasColumnName("fecha_recepcion").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.CantidadKg).HasColumnName("cantidad_kg").HasColumnType("decimal(10, 2)").IsRequired();
            entity.Property(e => e.Calidad).HasColumnName("calidad").HasMaxLength(1);
            entity.Property(e => e.UrlImagen).HasColumnName("url_imagen").HasColumnType("text");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_proveedor");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("movimientos");

            entity.HasKey(e => e.IdMovimiento).HasName("movimientos_pkey");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.IdLote).HasColumnName("id_lote").IsRequired();
            entity.Property(e => e.IdDestino).HasColumnName("id_destino").IsRequired();
            entity.Property(e => e.FechaEnvio).HasColumnName("fecha_envio").HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Transporte).HasColumnName("transporte").HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasColumnName("observaciones").HasColumnType("text");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdLote)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_lote");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_destino");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
