using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyFi.Models
{
    public partial class finanzasContext : DbContext
    {
        public finanzasContext()
        {
        }

        public finanzasContext(DbContextOptions<finanzasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<Gasto> Gastos { get; set; } = null!;
        public virtual DbSet<Ingreso> Ingresos { get; set; } = null!;
        public virtual DbSet<Texto> Textos { get; set; } = null!;
        public virtual DbSet<TipoGasto> TipoGastos { get; set; } = null!;
        public virtual DbSet<TipoIngreso> TipoIngresos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//              optionsBuilder.UseMySql("server=new.c2kbets.com;port=3306;uid=jimmy;password=Jimmy12345;database=finanzas", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.41-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracion)
                    .HasName("PRIMARY");

                entity.ToTable("configuracion");

                entity.Property(e => e.IdConfiguracion)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id_configuracion");

                entity.Property(e => e.Notificaciones).HasColumnName("notificaciones");

                entity.Property(e => e.Tema).HasColumnName("tema");

                entity.HasOne(d => d.IdConfiguracionNavigation)
                    .WithOne(p => p.Configuracion)
                    .HasForeignKey<Configuracion>(d => d.IdConfiguracion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("configuracion_ibfk_1");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(e => e.IdGastos)
                    .HasName("PRIMARY");

                entity.ToTable("gastos");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.HasIndex(e => e.IdTipo, "tipo");

                entity.Property(e => e.IdGastos)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_gastos");

                entity.Property(e => e.Automatico)
                    .HasColumnName("automatico")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IdTipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_tipo");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.Notificacion)
                    .HasColumnName("notificacion")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Recurrencia).HasColumnName("recurrencia");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("gastos_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("gastos_ibfk_1");
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.Id_Ingresos)
                    .HasName("PRIMARY");

                entity.ToTable("ingresos");

                entity.HasIndex(e => e.Id, "id");

                entity.Property(e => e.Id_Ingresos)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_ingresos");

                entity.Property(e => e.Automatico).HasColumnName("automatico");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.Notificacion).HasColumnName("notificacion");

                entity.Property(e => e.Recurrencia).HasColumnName("recurrencia");

                entity.Property(e => e.Tipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("ingresos_ibfk_1");
            });

            modelBuilder.Entity<Texto>(entity =>
            {
                entity.HasKey(e => e.IdTexto)
                    .HasName("PRIMARY");

                entity.ToTable("textos");

                entity.Property(e => e.IdTexto)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_texto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoGasto>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_gasto");

                entity.HasIndex(e => e.Tipo, "tipo");

                entity.Property(e => e.IdTipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_tipo");

                entity.Property(e => e.Tipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("tipo");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.TipoGastos)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tipo_gasto_ibfk_1");
            });

            modelBuilder.Entity<TipoIngreso>(entity =>
            {
                entity.HasKey(e => e.IdIngreso)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_ingreso");

                entity.HasIndex(e => e.Tipo, "tipo");

                entity.Property(e => e.IdIngreso)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_ingreso");

                entity.Property(e => e.Tipo)
                    .HasColumnType("int(11)")
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.IdIngresoNavigation)
                    .WithOne(p => p.TipoIngreso)
                    .HasForeignKey<TipoIngreso>(d => d.IdIngreso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_ingreso_ibfk_1");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.TipoIngresos)
                    .HasForeignKey(d => d.Tipo)
                    .HasConstraintName("tipo_ingreso_ibfk_2");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .HasColumnType("int(11)")
                    .HasColumnName("codigo");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(255)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Correo)
                    .HasMaxLength(255)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .HasColumnType("int(11)")
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.Validado).HasColumnName("validado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
