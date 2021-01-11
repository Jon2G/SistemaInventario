using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;
using Inventario.ViewModels.EntradasSalidas;
using Kit;
using Kit.Enums;
using System.Linq;
using System.Collections.Generic;
using static Kit.Extensions.Helpers;

namespace Inventario.ViewModels.EntradasSalidas
{
    public class EntradaSalida : ViewModelBase<EntradaSalida>
    {
        private ObservableCollection<Producto> _Productos;
        public ObservableCollection<Producto> Productos { get => _Productos; set { _Productos = value; OnPropertyChanged(); } }
        private ObservableCollection<AjusteInventario> _Ajustes;
        public ObservableCollection<AjusteInventario> Ajustes { get => _Ajustes; set { _Ajustes = value; OnPropertyChanged(); } }
        private Producto _Seleccion;
        public Producto Seleccion { get => _Seleccion; set { _Seleccion = value; OnPropertyChanged(); } }
        public ObservableCollection<Concepto> _Conceptos;
        public ObservableCollection<Concepto> Conceptos { get => _Conceptos; set { _Conceptos = value; OnPropertyChanged(); } }
        private readonly Tipo TipoAjuste;
        private float? _Cantidad;
        public float? Cantidad
        {
            get => _Cantidad;
            set
            {
                _Cantidad = value; 
                OnPropertyChanged();
            }
        }
        private Concepto _Concepto;
        public Concepto Concepto { get => _Concepto; set { _Concepto = value; OnPropertyChanged(); } }
        private string _Observaciones;
        public string Observaciones { get => _Observaciones; set { _Observaciones = value; OnPropertyChanged(); } }
        private bool _Imprimir;
        public bool Imprimir { get => _Imprimir; set { _Imprimir = value; OnPropertyChanged(); } }


        public EntradaSalida(Tipo TipoAjuste)
        {
            this.TipoAjuste = TipoAjuste;
            this.ListarProductos();
            switch (TipoAjuste)
            {
                case Tipo.Entrada:
                    this.ConceptosEntradasInventario();
                    break;
                case Tipo.Salida:
                    this.ConceptosSalidasInventario();
                    break;
            }
            this.Ajustes = new ObservableCollection<AjusteInventario>();
        }
        private void ListarProductos()
        {
            this.Productos = new ObservableCollection<Producto>(Producto.Listar());
        }

        internal void Agregar()
        {
            if (Cantidad is null || Cantidad <= 0)
            {
                Kit.Services.CustomMessageBox.Current.Show("La cantidad de este movimiento es invalida.", "Atención",
                    CustomMessageBoxButton.OK, CustomMessageBoxImage.Information);
                return;
            }

            if (this.Ajustes.FirstOrDefault(x => x.CodigoProducto == this.Seleccion.Codigo) is AjusteInventario ajuste)
            {
                ajuste.Cantidad += (float)this.Cantidad;
                ajuste.ExistenciaPosterior = (this.TipoAjuste == Tipo.Entrada) ?
                ajuste.ExistenciaActual + ajuste.Cantidad : ajuste.ExistenciaActual - ajuste.Cantidad;
                Seleccion = null;
                this.Cantidad = null;
                return;
            }
            float inventarioActual = Producto.ObtenerExistencia(Seleccion.Codigo);

            this.Ajustes.Add(new AjusteInventario()
            {
                CodigoProducto = Seleccion.Codigo,
                Cantidad = (float)this.Cantidad,
                Nombre = Seleccion.Nombre,
                ExistenciaActual = inventarioActual,
                ExistenciaPosterior = (this.TipoAjuste == Tipo.Entrada) ?
                inventarioActual + (float)this.Cantidad : inventarioActual - (float)this.Cantidad
            });
            Seleccion = null;
            this.Cantidad = null;
        }

        private void ConceptosEntradasInventario()
        {
            this.Conceptos = new ObservableCollection<Concepto>();
            this.Conceptos.Add(new Concepto("COM", "Compra"));
            this.Conceptos.Add(new Concepto("A+", "Ajuste"));
            this.Conceptos.Add(new Concepto("DV", "Devolución"));

        }
        private void ConceptosSalidasInventario()
        {
            this.Conceptos = new ObservableCollection<Concepto>();
            this.Conceptos.Add(new Concepto("DEFEC", "Defecto"));
            this.Conceptos.Add(new Concepto("CADUC", "Caducidad"));
            this.Conceptos.Add(new Concepto("MERMA", "Merma"));

        }
        public void Finalizar()
        {
            List<Movimiento> movimientos = new List<Movimiento>();
            foreach (AjusteInventario partida in this.Ajustes)
            {
                Movimiento movimiento = new Movimiento(partida.CodigoProducto, App.Usuario.Id, this.TipoAjuste, partida.Cantidad, partida.ExistenciaActual, partida.ExistenciaPosterior, this.Concepto.Clave, DateTime.Now);
                movimiento.RegistrarMovimiento();
                movimientos.Add(movimiento);
            }
            if (this.Imprimir)
            {
                Reporte.Movimiento(movimientos.ToTable());
            }
        }
    }
}
