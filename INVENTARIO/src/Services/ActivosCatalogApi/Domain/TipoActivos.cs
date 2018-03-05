using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosCatalogApi.Domain
{
    public class TipoActivos
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int VidaUtil { get; set; }
        public int Residual { get; set; }

    }
}
