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
                Conexion.Sqlite.FormatTime((DateTime)fechaInicial) +
                " AND MOVIMIENTOS.FECHA <=" +
                Conexion.Sqlite.FormatTime((DateTime)fechaFinal), "CONSULTA");
            if (consulta.Rows.Count <= 0)
            {
                MessageBox.Show("No se encontrarón movimiento en el rango de fechas seleccionado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //enviarla al reporteador
        }
    }
}
