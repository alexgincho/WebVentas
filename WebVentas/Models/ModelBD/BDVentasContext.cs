using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class BDVentasContext : DbContext
    {
        public BDVentasContext()
        {
        }

        public BDVentasContext(DbContextOptions<BDVentasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-0Q8CQPEA;Database=BDVentas; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.PkCategoria);

                entity.Property(e => e.PkCategoria).HasColumnName("pkCategoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCrea");

                entity.Property(e => e.FechaEdita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEdita");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fkUsuarioCrea");

                entity.Property(e => e.FkUsuarioEdita).HasColumnName("fkUsuarioEdita");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.PkContacto);

                entity.ToTable("Contacto");

                entity.Property(e => e.PkContacto).HasColumnName("pkContacto");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidoMaterno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellidoPaterno");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Documento)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("documento");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCrea");

                entity.Property(e => e.FechaEdita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEdita");

                entity.Property(e => e.FkTipoDocumento).HasColumnName("fkTipoDocumento");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fkUsuarioCrea");

                entity.Property(e => e.FkUsuarioEdita).HasColumnName("fkUsuarioEdita");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Ubigeo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ubigeo");

                entity.HasOne(d => d.FkTipoDocumentoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.FkTipoDocumento)
                    .HasConstraintName("FK_Contacto_TipoDocumento");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PkProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.PkProducto).HasColumnName("pkProducto");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCrea");

                entity.Property(e => e.FechaEdita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEdita");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaVencimiento");

                entity.Property(e => e.FkCategoria).HasColumnName("fkCategoria");

                entity.Property(e => e.FkProveedor).HasColumnName("fkProveedor");

                entity.Property(e => e.FkUnidad).HasColumnName("fkUnidad");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fkUsuarioCrea");

                entity.Property(e => e.FkUsuarioEdita).HasColumnName("fkUsuarioEdita");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombreProducto");

                entity.Property(e => e.PrecioCompra)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioCompra");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precioVenta");

                entity.HasOne(d => d.FkCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkCategoria)
                    .HasConstraintName("FK_Producto_Categoria");

                entity.HasOne(d => d.FkProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkProveedor)
                    .HasConstraintName("FK_Producto_Proveedor");

                entity.HasOne(d => d.FkUnidadNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkUnidad)
                    .HasConstraintName("FK_Producto_UnidadMedida");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.PkProveedor);

                entity.ToTable("Proveedor");

                entity.Property(e => e.PkProveedor).HasColumnName("pkProveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCrea");

                entity.Property(e => e.FechaEdita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEdita");

                entity.Property(e => e.FkContacto).HasColumnName("fkContacto");

                entity.Property(e => e.FkTipoDocumento).HasColumnName("fkTipoDocumento");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fkUsuarioCrea");

                entity.Property(e => e.FkUsuarioEdita).HasColumnName("fkUsuarioEdita");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("razonSocial");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Ubigeo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ubigeo");

                entity.HasOne(d => d.FkContactoNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.FkContacto)
                    .HasConstraintName("FK_Proveedor_Contacto");

                entity.HasOne(d => d.FkTipoDocumentoNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.FkTipoDocumento)
                    .HasConstraintName("FK_Proveedor_TipoDocumento");
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.PkTipoDocumento);

                entity.ToTable("TipoDocumento");

                entity.Property(e => e.PkTipoDocumento).HasColumnName("pkTipoDocumento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<UnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.PkUnidad);

                entity.Property(e => e.PkUnidad).HasColumnName("pkUnidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
