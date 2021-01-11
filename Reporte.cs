using Inventario.ViewModels.EntradasSalidas;
using Inventario.Views;
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
    public static class Reporte
    {

        public static void Existencia()
        {
            FechasReporte reporte = new FechasReporte();
            reporte.ShowDialog();

            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                printf('%.2f',PRODUCTOS.EXISTENCIA) AS EXISTENCIA,
                strftime('%d/%m/%Y',MOVIMIENTOS.FECHA) as 'FECHA'
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE JulianDay(MOVIMIENTOS.FECHA) >=JulianDay('" +
                SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaInicial) +
                "') AND JulianDay(MOVIMIENTOS.FECHA) <=JulianDay('" +
                SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaFinal) + "')", "CONSULTA");
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
            //   , new Variable(consulta)
            //    , new Variable("FECHA_INICIAL", reporte.FechaInicial)
            //    , new Variable("FECHA_FINAL", reporte.FechaFinal));

            ////función para abrir el diseñador del reporte - COMPAÑEROS
            reporteador.MostrarReporte("ReporteExistencia.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", reporte.FechaInicial)
                , new Variable("FECHA_FINAL", reporte.FechaFinal));
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
        public static void Movimientos()
        {
            FechasReporte reporte = new FechasReporte();
            reporte.ShowDialog();
            //MOVIMIENTOS.NUMERO, no se si es numero
            DataTable consulta = Conexion.Sqlite.DataTable(
                @"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.TIPO,
                printf('%.2f',MOVIMIENTOS.CANTIDAD) AS CANTIDAD,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_ACTUAL) AS EXISTENCIA_ACTUAL,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_POSTERIOR) AS EXISTENCIA_POSTERIOR  
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE JulianDay(MOVIMIENTOS.FECHA) >=JulianDay('" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaInicial) +
                "') AND JulianDay(MOVIMIENTOS.FECHA) <=JulianDay('" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaFinal) + "')", "CONSULTA");
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
                , new Variable("FECHA_INICIAL", (DateTime)reporte.FechaInicial)
                , new Variable("FECHA_FINAL", (DateTime)reporte.FechaFinal));
        }
        public static void EntradasSalidas(Tipo Tipo)
        {
            FechasReporte reporte = new FechasReporte();
            reporte.ShowDialog();

            DataTable consulta = Conexion.Sqlite.DataTable(
                $@"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.ID_USUARIO,
                printf('%.2f',MOVIMIENTOS.CANTIDAD),
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_POSTERIOR),
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO
                WHERE TIPO='{(Tipo == Tipo.Entrada ? 'E' : 'S')}' AND MOVIMIENTOS.FECHA >=JUALIANDAY('" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaInicial) +
                "') AND JUALIANDAY(MOVIMIENTOS.FECHA) <=JUALIANDAY('" +
                 SQLHelper.SQLHelper.FormatTime((DateTime)reporte.FechaFinal) + "')", "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string rutamrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            string rutalogo = rutamrt +  @"\inventario.png";
            Reporteador reporteador = new Reporteador(rutalogo, rutamrt);
            reporteador.MostrarReporte((Tipo == Tipo.Entrada ? "ReporteEntradas.mrt" : "ReporteSalidas.mrt") 
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", (DateTime)reporte.FechaInicial)
                , new Variable("FECHA_FINAL", (DateTime)reporte.FechaFinal));
        }
        public static void Entradas()
        {
            EntradasSalidas(Tipo.Entrada);
        }
        public static void Salidas()
        {
            EntradasSalidas(Tipo.Salida);
        }
    }
}
