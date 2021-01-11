using Kit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.ViewModels.EntradasSalidas
{
    public class ProductoInvis : ViewModelBase<ProductoInvis>
    {
        public int Id { get; set; }
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }

        public float ExistenciaTeorica { get; set; }
        private float? _ExistenciaReal;

        public float? ExistenciaReal
        {
            get => _ExistenciaReal;
            set
            {
                _ExistenciaReal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Editado));
            }
        }
        public bool Editado => ExistenciaReal != null && ExistenciaTeorica != ExistenciaReal;

        public ProductoInvis(int Id,string CodigoProducto, string Nombre, string Unidad, float ExistenciaTeorica)
        {
            this.Id = Id;
            this.CodigoProducto = CodigoProducto;
            this.Nombre = Nombre;
            this.Unidad = Unidad;
            this.ExistenciaReal = null;
            this.ExistenciaTeorica = ExistenciaTeorica;
        }

        public ProductoInvis()
        {
        }
    }
}
