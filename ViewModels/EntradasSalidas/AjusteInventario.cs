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
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        private float _Cantidad;

        public float Cantidad
        {
            get => _Cantidad;
            set
            {
                _Cantidad = value;OnPropertyChanged();
            }
        }

        private float _CantidadVariable;

        public float CantidadVariable
        {
            get => _CantidadVariable;
            set
            {
                _CantidadVariable = value; 
                OnPropertyChanged();
            }
        }

        


        public float ExistenciaActual { get; set; }
        private float _InventarioF;
        public float ExistenciaPosterior
        {
            get => _InventarioF;
            set 
            {
                _InventarioF = value;OnPropertyChanged(); 
            }
        }

        public AjusteInventario()
        {

        }
    }
}
