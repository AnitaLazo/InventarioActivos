using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivosCatalogApi.Data;
using ActivosCatalogApi.Domain;
using INVENTARIO.Services.ActivosCatalogApi;
using INVENTARIO.Services.ActivosCatalogApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ActivosCatalogApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CatalogActivos")]
    public class CatalogActivosController : Controller
    {
        private readonly ActivosContext _catalogcontext;
        private readonly IOptionsSnapshot<ActivosSettings> _settings;
        public CatalogActivosController(ActivosContext catalogcontext, IOptionsSnapshot<ActivosSettings> settings)
        {
            _catalogcontext = catalogcontext;
            _settings = settings;
            ((DbContext)catalogcontext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
            string url = settings.Value.ExternalActivosBaseUrl;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Tipo()
        {
            var activos = await _catalogcontext.TipoActivos.ToListAsync();
            return Ok(activos);
        }

        [HttpGet]
        [Route("activos/{id:int}")]
        public async Task<IActionResult> GetActivosById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var item = await _catalogcontext.CatalogActivos.SingleOrDefaultAsync(c => c.Id == id);
            if(item != null)
            {
                return Ok(item);
                
            }
            return NotFound();
        }

        //GET api/Catalog/activos[?pageSize=4&pageIndex=3]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Activos([FromQuery] int pageSize=6, [FromQuery] int pageIndex = 0)
        {
            var totalactivos = await _catalogcontext.CatalogActivos
                .LongCountAsync();
            var activosOnPage = await _catalogcontext.CatalogActivos
                .OrderBy(c => c.Detalle)
                .Skip(pageSize = pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var model = new PaginateItemsViewModel<CatalogActivos>(pageIndex, pageSize, totalactivos, activosOnPage);

            return Ok(model);
        }

        //GET api/Catalog/activos/withdetalle?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withdetalle/{detalle:minlength(1)}")]
        public async Task<IActionResult> Activos(string detalle,[FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var totalactivos = await _catalogcontext.CatalogActivos
                .Where(c=> c.Detalle.StartsWith(detalle))
                .LongCountAsync();
            var activosOnPage = await _catalogcontext.CatalogActivos
                .Where(c => c.Detalle.StartsWith(detalle))
                .OrderBy(c => c.Detalle)
                .Skip(pageSize = pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var model = new PaginateItemsViewModel<CatalogActivos>(pageIndex, pageSize, totalactivos, activosOnPage);

            return Ok(model);
        }

        //GET api/Catalog/activos/tipo/1/[?pageSize=4&pageIndex=0]
        [HttpGet]
        [Route("[action]/tipo/{tipoactivoId}")]
        public async Task<IActionResult> Activos(int ? tipoactivoId, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var root =(IQueryable<CatalogActivos>) _catalogcontext.CatalogActivos;

            if (tipoactivoId.HasValue)
            {
                root = root.Where(c => c.TipoActivosID == tipoactivoId);
            }
            var totalactivos = await root
                
                .LongCountAsync();
            var activosOnPage = await root
                .OrderBy(c => c.Detalle)
                .Skip(pageSize = pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var model = new PaginateItemsViewModel<CatalogActivos>(pageIndex, pageSize, totalactivos, activosOnPage);

            return Ok(model);
        }

        [HttpPost]
        [Route("activos")]
        public async Task<IActionResult> CreateActivos([FromBody] CatalogActivos activo) {
            var item = new CatalogActivos
            {
                TipoActivosID = activo.TipoActivosID,
                Detalle = activo.Detalle,
                Marca = activo.Marca,
                Serie_modelo = activo.Serie_modelo,
                Precio_compra = activo.Precio_compra,
                Fecha_adquisicion = activo.Fecha_adquisicion,
                Acta_entrega = activo.Acta_entrega,
                Cod_institucion = activo.Cod_institucion,
                Estado = activo.Estado,
                Observacion = activo.Observacion,
                Precio_actual = activo.Precio_actual
            };
            _catalogcontext.CatalogActivos.Add(item);
             await _catalogcontext.SaveChangesAsync();
           // _catalogcontext.SaveChanges();
            return CreatedAtAction(nameof (GetActivosById),new { id= item.Id} );
        }

        [HttpPut]
        [Route("activos")]
        public async Task<IActionResult> UpdateActvio([FromBody] CatalogActivos activoToUpdate)
        {
            var catalogActivo = await _catalogcontext.CatalogActivos
                .SingleOrDefaultAsync(i => i.Id == activoToUpdate.Id);

            if (catalogActivo == null)
            {
                return NotFound(new { Message = $"El activo con id {activoToUpdate.Id} not found." });

            }
            catalogActivo = activoToUpdate;
            _catalogcontext.CatalogActivos.Update(catalogActivo);
            await _catalogcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetActivosById), new { id = activoToUpdate.Id });

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteActivo(int id)
        {
            var activo = await _catalogcontext.CatalogActivos.SingleOrDefaultAsync(p => p.Id == id);

            if (activo == null)
            {
                return NotFound();

            }

            _catalogcontext.CatalogActivos.Remove(activo);
            await _catalogcontext.SaveChangesAsync();
            return NoContent();
        }



    }
}