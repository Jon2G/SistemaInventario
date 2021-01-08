using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    public class Reporte
    {
        public string Titulo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int NumeroMovimiento { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string ClasificacionProducto { get; set; }
        public char TipoMovimiento { get; set; }
        public float CantidadMovimiento { get; set; }
        public float ExistenciaResultante { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
