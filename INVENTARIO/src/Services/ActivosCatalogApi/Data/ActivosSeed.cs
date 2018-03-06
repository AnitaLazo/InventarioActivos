using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivosCatalogApi.Data;
using ActivosCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace INVENTARIO.Services.ActivosCatalogApi.Data
{
    public class ActivosSeed
    {

        public static async Task SeedAsync(ActivosContext context)


      //  public static void SeedAsync(ActivosContext context)
        {
            //esto sirve para que se cree la migracion automaticamente
            context.Database.Migrate();
            if (!context.TipoActivos.Any())
            {
                context.TipoActivos.AddRange(GetPreconfiguredTipoActivos());
                await context.SaveChangesAsync();
                //context.SaveChanges();

            }
            if (!context.CatalogActivos.Any())
            {
                context.CatalogActivos.AddRange(GetPreconfiguredCatalogActivos());
                 await context.SaveChangesAsync();
                //context.SaveChanges();
            }

        }
        static IEnumerable<TipoActivos> GetPreconfiguredTipoActivos()
        {
            return new List<TipoActivos>()
            {
                new TipoActivos(){Tipo="Construcciones y Edificios", VidaUtil=20, Residual=10},
                new TipoActivos(){Tipo="Instalaciones", VidaUtil=10, Residual=10},
                new TipoActivos(){Tipo="Maquinaria y Equipo", VidaUtil=10, Residual=10},
                new TipoActivos(){Tipo="Herramientas", VidaUtil=10, Residual=10},
                new TipoActivos(){Tipo="Vehiculos", VidaUtil=5, Residual=10},
                new TipoActivos(){Tipo="Muebles y Enseres", VidaUtil=10, Residual=10},
                new TipoActivos(){Tipo="Equipo de Oficina", VidaUtil=10, Residual=10},
                new TipoActivos(){Tipo="Equipos de computo", VidaUtil=3, Residual=10},
            };

        }

        static IEnumerable<CatalogActivos> GetPreconfiguredCatalogActivos()
        {
            return new List<CatalogActivos>()
            {
                new CatalogActivos(){Detalle="Mouse",Marca="Genius",Serie_modelo="HAG5",TipoActivosID=8,Acta_entrega="M15",Cod_institucion="ULEAM11",Fecha_adquisicion=new DateTime (2015,10,5),Estado="Activo",Precio_compra=new decimal(15.00),Precio_actual=new decimal(5.00)}
            };
        }

    }
}
