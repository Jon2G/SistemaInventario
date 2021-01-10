using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.ViewModels.EntradasSalidas
{
    public struct Concepto
    {
        public string Descripcion { get; set; }
        public string Clave { get; set; }

        public Concepto(string Clave, string Descripcion)
        {
            this.Clave = Clave;
            this.Descripcion = Descripcion;
        }
    }
}
