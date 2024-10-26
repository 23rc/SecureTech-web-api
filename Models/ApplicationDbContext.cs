using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SecureTech_web_api.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<paciente> paciente { get; set; }

    public virtual DbSet<resultadoDePrueba> resultadoDePrueba { get; set; }

    public virtual DbSet<rol> rol { get; set; }

    public virtual DbSet<tratamiento> tratamiento { get; set; }

    public virtual DbSet<usuario> usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source='DESKTOP-35E850O\\SQLEXPRESS';Initial Catalog='SecureTech-bd';Persist Security Info=True;User ID=sa;password=sqlserver;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<paciente>(entity =>
        {
            entity.HasKey(e => e.codigoPaciente).HasName("pk_paciente_codigoPaciente");

            entity.Property(e => e.nombreCompleto).HasMaxLength(128);
        });

        modelBuilder.Entity<resultadoDePrueba>(entity =>
        {
            entity.HasKey(e => e.codigoResultado).HasName("pk_resultadoDePrueba_codigoResultado");

            entity.Property(e => e.resultado).HasMaxLength(255);
            entity.Property(e => e.tipoPrueba).HasMaxLength(128);

            entity.HasOne(d => d.codigoPacienteNavigation).WithMany(p => p.resultadoDePrueba)
                .HasForeignKey(d => d.codigoPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_resultadoDePrueba_codigoPaciente");
        });

        modelBuilder.Entity<rol>(entity =>
        {
            entity.HasKey(e => e.codigoRol).HasName("pk_rol_codigoRol");

            entity.HasIndex(e => e.nombreRol, "uq_rol_nombreRol").IsUnique();

            entity.Property(e => e.nombreRol).HasMaxLength(128);
        });

        modelBuilder.Entity<tratamiento>(entity =>
        {
            entity.HasKey(e => e.codigoTratamiento).HasName("pk_tratamiento_codigoTratamiento");

            entity.Property(e => e.descripcion).HasMaxLength(255);

            entity.HasOne(d => d.codigoPacienteNavigation).WithMany(p => p.tratamiento)
                .HasForeignKey(d => d.codigoPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tratamiento_codigoPaciente");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.codigoUsuario).HasName("pk_usuario_codigoUsuario");

            entity.HasIndex(e => e.clave, "uq_usuario_clave").IsUnique();

            entity.HasIndex(e => e.correo, "uq_usuario_correo").IsUnique();

            entity.Property(e => e.clave).HasMaxLength(255);
            entity.Property(e => e.correo).HasMaxLength(128);
            entity.Property(e => e.fechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.codigoRolNavigation).WithMany(p => p.usuario)
                .HasForeignKey(d => d.codigoRol)
                .HasConstraintName("fk_usuario_codigoRol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
