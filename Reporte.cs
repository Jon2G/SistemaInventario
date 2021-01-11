using Kit.WPF.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public static void Existencia(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                PRODUCTOS.EXISTENCIA,
                MOVIMIENTOS.FECHA
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE MOVIMIENTOS.FECHA >=" +
                SQLHelper.SQLHelper.FormatTime((DateTime)fechaInicial) +
                " AND MOVIMIENTOS.FECHA <=" +
                SQLHelper.SQLHelper.FormatTime((DateTime)fechaFinal), "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt + @"\inventario.png";

            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);

            //función para abrir el diseñador del reporte. - NOSOTROS
            //reporteador.NuevoReporte("ReporteExistencia.mrt", true
            //    , new Variable(consulta)
            //    , new Variable("FECHA_INICIAL", fechaInicial)
            //    , new Variable("FECHA_FINAL", fechaFinal));

            //función para abrir el diseñador del reporte - COMPAÑEROS
            reporteador.MostrarReporte("ReporteExistencia.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", fechaInicial)
                , new Variable("FECHA_FINAL", fechaFinal));
        }
        public static void Movimiento(DataTable movimientos)
        {
            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt + @"\inventario.png";
            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);
            reporteador.MostrarReporte("ReporteMovimiento.mrt"
                , new Variable(movimientos)
                , new Variable("FECHA", DateTime.Now));
        }
        public static void Movimientos(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            //MOVIMIENTOS.NUMERO, no se si es numero
            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.TIPO,
                MOVIMIENTOS.CANTIDAD
                MOVIMIENTOS.EXISTENCIA_POSTERIOR, 
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE MOVIMIENTOS.FECHA >=" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)fechaInicial) +
                " AND MOVIMIENTOS.FECHA <=" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)fechaFinal), "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt + @"\inventario.png";
            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);
            reporteador.MostrarReporte("ReporteMovimientos.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", fechaInicial)
                , new Variable("FECHA_FINAL", fechaFinal));
        }
        public static void Entradas(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.ID_USUARIO,
                MOVIMIENTOS.CANTIDAD,
                MOVIMIENTOS.EXISTENCIA_POSTERIOR,
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE TIPO='E' AND MOVIMIENTOS.FECHA >=" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)fechaInicial) +
                " AND MOVIMIENTOS.FECHA <=" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)fechaFinal), "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt + @"\inventario.png";
            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);
            reporteador.MostrarReporte("ReporteEntradas.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", fechaInicial)
                , new Variable("FECHA_FINAL", fechaFinal));

        }
        public static void Salidas(DateTime? fechaInicial, DateTime? fechaFinal)
        {

            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                MOVIMIENTOS.NUMERO,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.ID_USUARIO,
                MOVIMIENTOS.CANTIDAD,
                MOVIMIENTOS.EXISTENCIA_POSTERIOR,
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE TIPO='S' AND MOVIMIENTOS.FECHA >=" +
                SQLHelper.SQLHelper.FormatTime((DateTime)fechaInicial) +
                " AND MOVIMIENTOS.FECHA <=" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)fechaFinal), "CONSULTA");

            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt + @"\inventario.png";
            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);
            reporteador.MostrarReporte("ReporteSalidas.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", fechaInicial)
                , new Variable("FECHA_FINAL", fechaFinal));
        }
    }
}
