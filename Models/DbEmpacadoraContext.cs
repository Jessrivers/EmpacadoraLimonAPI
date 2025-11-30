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
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(255);
            entity.Property(e => e.Latitud).HasColumnName("latitud");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
            entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.ToTable("destinos");

            entity.HasKey(e => e.IdDestino).HasName("destinos_pkey");

            entity.Property(e => e.IdDestino).HasColumnName("id_destino");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(255);
            entity.Property(e => e.Latitud).HasColumnName("latitud");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.ToTable("lotes");

            entity.HasKey(e => e.IdLote).HasName("lotes_pkey");

            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.CantidadKg).HasColumnName("cantidad_kg");
            entity.Property(e => e.Calidad).HasColumnName("calidad").HasMaxLength(1);
            entity.Property(e => e.UrlImagen).HasColumnName("url_imagen");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("lotes_id_proveedor_fkey");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("movimientos");

            entity.HasKey(e => e.IdMovimiento).HasName("movimientos_pkey");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.IdDestino).HasColumnName("id_destino");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Transporte).HasColumnName("transporte").HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("movimientos_id_lote_fkey");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdDestino)
                .HasConstraintName("movimientos_id_destino_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
