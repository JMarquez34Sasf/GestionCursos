using System;
using System.Collections.Generic;
using GestionCursos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionCursos.Data;

public partial class GestionCursosContext : DbContext
{
    public GestionCursosContext()
    {
    }

    public GestionCursosContext(DbContextOptions<GestionCursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__085F27D627E7636B");

            entity.ToTable("Curso");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(150);

            entity.HasOne(d => d.IdInstructorNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdInstructor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Curso_Instructor");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__A122F2BF21EF71FF");

            entity.ToTable("Inscripcion");

            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaInscripcion).HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripcion_Curso");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripcion_Estudiante");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9790C863B0");

            entity.ToTable("Usuario");

            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
