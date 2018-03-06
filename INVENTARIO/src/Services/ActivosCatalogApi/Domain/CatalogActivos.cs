using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosCatalogApi.Domain
{
    public class CatalogActivos
    {
        [Key]
        public int Id { get; set; }
        public string Detalle { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_adquisicion { get; set; }
        public string Marca { get; set; }
        public string Serie_modelo { get; set; }
        public string Observacion { get; set; }
        public string Acta_entrega { get; set; }
        public string Cod_institucion { get; set; }
        public string Estado { get; set; }
        public decimal Precio_compra { get; set; }
        public decimal Precio_actual { get; set; }

        public int TipoActivosID { get; set; }
        //public TipoActivos TipoActivos { get; set; }


    }
}
