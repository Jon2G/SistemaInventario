using Inventario.ViewModels.EntradasSalidas;
using Inventario.Views;
using Kit.Enums;
using Kit.WPF.Controls.RangoFechas;
using Kit.WPF.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kit.Controls.DateRange;
using Kit.Sql.Helpers;
using Kit.WPF.Dialogs.ICustomMessageBox;

namespace Inventario
{
    public static class Reporte
    {
        private static string RutaMrt;
        private static string RutaLogo;
        static Reporte()
        {
            Reporte.RutaMrt = Kit.Tools.Instance.LibraryPath + @"\mrt";
            Reporte.RutaLogo = Reporte.RutaMrt + @"\inventario.png";

        }
        private static Rango Fechas()
        {
            FechasReporte reporte = new FechasReporte();
            reporte.ShowDialog();
            return reporte.Rango;
        }
        public static void Existencia()
        {
            DataTable consulta = Conexion.Sqlite.DataTable(
                $@"SELECT 
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                printf('%.2f',PRODUCTOS.EXISTENCIA) AS EXISTENCIA,
				IFNULL(strftime('%d/%m/%Y',(SELECT MAX(JULIANDAY(M.FECHA)) FROM MOVIMIENTOS M WHERE PRODUCTOS.ID = M.ID_PRODUCTO )),'S/M') as 'FECHA'
				 FROM PRODUCTOS WHERE PRODUCTOS.OCULTO=0", "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                SinMovimientos();
                return;
            }


            Reporteador reporteador = new Reporteador(Reporte.RutaLogo, Reporte.RutaMrt);

            //función para abrir el diseñador del reporte. - NOSOTROS
            //reporteador.NuevoReporte("ReporteExistencia.mrt", true
            //   , new Variable(consulta)
            //    , new Variable("FECHA_INICIAL", rango.Inicio)
            //    , new Variable("FECHA_FINAL", rango.Fin));

            ////función para abrir el diseñador del reporte - COMPAÑEROS
            reporteador.MostrarReporte("ReporteExistencia.mrt", new Variable(consulta));
        }
        public static void Movimiento(DataTable movimientos, string Observaciones, string Concepto)
        {
            Reporteador reporteador = new Reporteador(Reporte.RutaLogo, Reporte.RutaMrt);
            reporteador.MostrarReporte("ReporteMovimiento.mrt"
                , new Variable(movimientos)
                , new Variable("Observaciones", Observaciones ?? string.Empty)
                , new Variable("Concepto", Concepto)
                , new Variable("Usuario", App.Usuario.Nombre)
                , new Variable("FECHA", DateTime.Now));
        }
        public static void Movimientos()
        {
            Rango rango = Fechas();
            if (rango.Cancelado) { return; }
            //MOVIMIENTOS.NUMERO, no se si es numero
            DataTable consulta = Conexion.Sqlite.DataTable(
                $@"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.TIPO,
                printf('%.2f',MOVIMIENTOS.CANTIDAD) AS CANTIDAD,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_ACTUAL) AS EXISTENCIA_ACTUAL,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_POSTERIOR) AS EXISTENCIA_POSTERIOR  
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO WHERE PRODUCTOS.OCULTO=0
                {(rango.TodasLasFechas ?
                "" : @" AND JulianDay(MOVIMIENTOS.FECHA) >= JulianDay('" +
                 SQLHelper.FormatTime((DateTime)rango.Inicio) +
                "') AND JulianDay(MOVIMIENTOS.FECHA) <=JulianDay('" +
                 SQLHelper.FormatTime((DateTime)rango.Fin) + "')")}", "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                SinMovimientos();
                return;
            }
            Reporteador reporteador = new Reporteador(Reporte.RutaLogo, Reporte.RutaMrt);
            reporteador.MostrarReporte("ReporteMovimientos.mrt"
                , new Variable(consulta)
                , new Variable("FECHA_INICIAL", (DateTime)rango.Inicio)
                , new Variable("FECHA_FINAL", (DateTime)rango.Fin));
        }

        private static void SinMovimientos()
        {
            CustomMessageBox.Show("No se encontrarón movimientos en el rango de fechas seleccionado", "Alerta", CustomMessageBoxButton.OK, CustomMessageBoxImage.Warning);
        }

        public static void EntradasSalidas(Tipo Tipo)
        {
            Rango rango = Fechas();
            if (rango.Cancelado) { return; }
            DataTable consulta = Conexion.Sqlite.DataTable(
                $@"SELECT 
                MOVIMIENTOS.ID,
                PRODUCTOS.CODIGO,
                PRODUCTOS.NOMBRE,
                PRODUCTOS.CLASIFICACION,
                MOVIMIENTOS.ID_USUARIO,
                printf('%.2f',MOVIMIENTOS.CANTIDAD) AS CANTIDAD,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_ACTUAL) AS EXISTENCIA_ANTERIOR,
                printf('%.2f',MOVIMIENTOS.EXISTENCIA_POSTERIOR) AS EXISTENCIA_POSTERIOR,
                USUARIOS.NOMBRE AS USUARIO
                FROM PRODUCTOS
                JOIN MOVIMIENTOS ON PRODUCTOS.ID = MOVIMIENTOS.ID_PRODUCTO 
                JOIN USUARIOS ON USUARIOS.ID=MOVIMIENTOS.ID_USUARIO WHERE PRODUCTOS.OCULTO=0 {(rango.TodasLasFechas ? "" : $@" AND TIPO='{(Tipo == Tipo.Entrada ? 'E' : 'S')}' AND JULIANDAY(MOVIMIENTOS.FECHA) >=JULIANDAY('" +
                 SQLHelper.FormatTime((DateTime)rango.Inicio) +
                "') AND JULIANDAY(MOVIMIENTOS.FECHA) <=JULIANDAY('" +
                 SQLHelper.FormatTime((DateTime)rango.Fin) + "')")}", "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                SinMovimientos();
                return;
            }
            Reporteador reporteador = new Reporteador(Reporte.RutaLogo, Reporte.RutaMrt);
            reporteador.MostrarReporte("ReporteEntradasSalidas.mrt"
                , new Variable(consulta)
                , new Variable("TIPO", Tipo.ToString())
                , new Variable("FECHA_INICIAL", (DateTime)rango.Inicio)
                , new Variable("FECHA_FINAL", (DateTime)rango.Fin));
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
