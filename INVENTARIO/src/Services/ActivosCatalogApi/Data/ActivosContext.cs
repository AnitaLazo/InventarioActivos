using ActivosCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosCatalogApi.Data
{
    public class ActivosContext : DbContext
    {
     
        public ActivosContext(DbContextOptions options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TipoActivos>(ConfigureTipoActivos);
            builder.Entity<CatalogActivos>(ConfigureCatalogActivos);
            
        }
        private void ConfigureTipoActivos(EntityTypeBuilder<TipoActivos> builder)
        {
            builder.ToTable("TipoActivos");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("TipoActivos_hilo")
                .IsRequired();
            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Residual)
                .IsRequired();
            builder.Property(c => c.VidaUtil)
                .IsRequired();

        }
        private void ConfigureCatalogActivos(EntityTypeBuilder<CatalogActivos> builder)
        {
            builder.ToTable("CatalogActivos");

            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("CatalogActivos_hilo")
                .IsRequired();
            builder.Property(c => c.Detalle)
                .IsRequired(true)
                .HasMaxLength(100);
            builder.Property(c => c.Fecha_adquisicion)
                .IsRequired(true);
            builder.Property(c => c.Cod_institucion)
                .IsRequired(true);
            builder.Property(c => c.Acta_entrega)
                .IsRequired(true);
            builder.Property(c => c.Estado)
                .IsRequired(false);
            builder.Property(c => c.Marca)
                .IsRequired(true);
            builder.Property(c => c.Observacion)
                .IsRequired(false);
            builder.Property(c => c.Precio_actual)
                .IsRequired(true);
            builder.Property(c => c.Precio_compra)
                .IsRequired(true);
            builder.Property(c => c.Serie_modelo)
                .IsRequired(true);
            builder.HasOne<TipoActivos>()
                .WithMany()
                .HasForeignKey(c => c.TipoActivosID);
            
        }



        public DbSet<CatalogActivos> CatalogActivos { get; set; }
        public DbSet<TipoActivos> TipoActivos { get; set; }

    }
}
