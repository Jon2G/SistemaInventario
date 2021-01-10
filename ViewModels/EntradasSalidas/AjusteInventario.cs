using Kit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.ViewModels.EntradasSalidas
{
    public class AjusteInventario : ViewModelBase<AjusteInventario>
    {
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        private double _Cantidad;

        public double Cantidad
        {
            get => _Cantidad;
            set
            {
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }
        private double _Precio;
        public double Precio { get => _Precio; set { _Precio = value; OnPropertyChanged("Precio"); } }
        private double _Importe;
        public double Importe
        {
            get => _Importe;
            set
            {
                _Importe = value;
                OnPropertyChanged("Importe");
            }
        }

        public double Inventario { get; set; }
        private double _InventarioF;
        public double InventarioF
        {
            get => _InventarioF;
            set { _InventarioF = value; OnPropertyChanged("InventarioF"); }
        }

        public AjusteInventario()
        {

        }
    }
}
