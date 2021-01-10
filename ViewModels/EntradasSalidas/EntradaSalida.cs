using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;
using Inventario.ViewModels.EntradasSalidas;
using Kit;

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
        private void GuardarEntrada(Concepto concepto, double importe, string observaciones, bool imprime)
        {
            //int ConsecEnt = Consecutivo.Siguiente("MovEnt");
            //CrearCabeceraEntradaInv(
            //    ConsecEnt, concepto.Descripcion, fecha, importe, (pendiete ? "PE" : "CO"), observaciones);

            //foreach (AjusteInventario entrada in this.Ajustes)
            //{
            //    int ConsecPartida = Consecutivo.Siguiente("entpart");
            //    AgregarPartidaEntradaAlmacen(
            //        ConsecEnt, ConsecPartida, concepto.Descripcion,
            //        entrada.Articulo, entrada.Cantidad, entrada.Precio, fecha, AlmacenActual.NAlmacen, pendiete);
            //}

            //if (imprime)
            //{
            //    System.Data.DataTable inventario = new System.Data.DataTable("PARTIDAS");
            //    inventario.Columns.AddRange(new[]
            //    {
            //    new DataColumn("ARTICULO",typeof(string)),
            //    new DataColumn("DESCRIPCION",typeof(string)),
            //    new DataColumn("CANTIDAD",typeof(double)),
            //    new DataColumn("PRECIO",typeof(double)),
            //    new DataColumn("POSTERIOR",typeof(double)),
            //    new DataColumn("FINAL",typeof(double))
            //});
            //    foreach (var x in this.Ajustes)
            //    {
            //        inventario.Rows.Add(
            //            x.Articulo, x.Descripcion, x.Cantidad,
            //            x.Precio, x.Inventario, x.InventarioF);
            //    }
            //    AppData.MRT.EntradaInventario(inventario,
            //    ConsecEnt, concepto.Clave, fecha, importe,
            //    this.AlmacenActual.NAlmacen, pendiete, observaciones);
            //}
            //this.Ajustes.Clear();
        }
        private void GuardarSalida(Concepto concepto, double importe, string observaciones, bool imprime)
        {
            //int ConsecEnt = Consecutivo.Siguiente("MovSal");
            //CrearCabeceraSalidaInv(
            //    ConsecEnt, concepto.Descripcion, fecha, importe, (pendiete ? "PE" : "CO"), observaciones);

            //foreach (AjusteInventario entrada in this.Ajustes)
            //{
            //    int ConsecPartida = Consecutivo.Siguiente("SALPART");
            //    AgregarPartidaSalidaAlmacen(ConsecEnt, ConsecPartida, concepto.Descripcion,
            //        entrada.Articulo, entrada.Cantidad, entrada.Precio, fecha, AlmacenActual.NAlmacen, pendiete);
            //}

            //if (imprime)
            //{
            //    DataTable inventario = new DataTable("PARTIDAS");
            //    inventario.Columns.AddRange(new[]
            //    {
            //    new DataColumn("ARTICULO",typeof(string)),
            //    new DataColumn("DESCRIPCION",typeof(string)),
            //    new DataColumn("CANTIDAD",typeof(double)),
            //    new DataColumn("PRECIO",typeof(double)),
            //    new DataColumn("POSTERIOR",typeof(double)),
            //    new DataColumn("FINAL",typeof(double))
            //});
            //    foreach (var x in this.Ajustes)
            //    {
            //        inventario.Rows.Add(
            //            x.Articulo, x.Descripcion, x.Cantidad,
            //            x.Precio, x.Inventario, x.InventarioF);

            //    }
            //    //AppData.MRT.SalidaInventario(inventario,
            //    //ConsecEnt, concepto.Clave, fecha, importe,

            //    this.Ajustes.Clear();
            //}
        }
        internal void Guardar(Concepto concepto, double importe, string observaciones, bool imprime)
        {
            switch (TipoAjuste)
            {
                case Tipo.Entrada:
                    GuardarEntrada(concepto, importe, observaciones, imprime);
                    break;
                case Tipo.Salida:
                    GuardarSalida(concepto, importe, observaciones, imprime);
                    break;
            }
        }
    }
}
