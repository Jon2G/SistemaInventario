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
                _Cantidad = value; OnPropertyChanged();
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
                if (_Cantidad != _CantidadVariable)
                {
                    _Cantidad = _CantidadVariable;
                    if(this.TipoAjuste== Tipo.Entrada)
                    {
                        this.ExistenciaPosterior = this.ExistenciaActual + _Cantidad;
                    }
                    else
                    {
                        this.ExistenciaPosterior = this.ExistenciaActual - _Cantidad;
                    }
                }
            }
        }
        public float ExistenciaActual { get; set; }
        private float _ExistenciaPosterior;
        public float ExistenciaPosterior
        {
            get => _ExistenciaPosterior;
            set
            {
                _ExistenciaPosterior = value;
                OnPropertyChanged();
            }
        }
        private readonly Tipo TipoAjuste;
        public AjusteInventario(string CodigoProducto, string Nombre,
            float Cantidad, float ExistenciaActual, float ExistenciaPosterior, Tipo TipoAjuste)
        {
            this.CodigoProducto = CodigoProducto;
            this.Nombre = Nombre;
            this.CantidadVariable =
            this.Cantidad = Cantidad;
            this.ExistenciaActual = ExistenciaActual;
            this.ExistenciaPosterior = ExistenciaPosterior;
            this.TipoAjuste = TipoAjuste;
        }
    }
}
