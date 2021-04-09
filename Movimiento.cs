
using Kit.Sql.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Sql.Helpers;

namespace Inventario
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        /// <summary>
        /// Letra que representa el tipo de movimiento ('S'/'E') (Salida/Entrada)
        /// </summary>
        public ViewModels.EntradasSalidas.Tipo Tipo { get; set; }
        public float Cantidad { get; set; }
        public float ExistenciaActual { get; set; }
        public float ExistenciaPosterior { get; set; }
        public string Concepto { get; set; }
        public DateTime Fecha { get; set; }

        public Movimiento(int IdMovimiento, int IdProducto, int IdUsuario,
            char Tipo, float Cantidad, float ExistenciaActual, float ExistenciaPosterior,
            string Concepto, DateTime Fecha)
        {
            this.IdMovimiento = IdMovimiento;
            this.IdProducto = IdProducto;
            this.IdUsuario = IdUsuario;
            this.Tipo = Tipo == 'E' ? ViewModels.EntradasSalidas.Tipo.Entrada : ViewModels.EntradasSalidas.Tipo.Salida;
            this.Cantidad = Cantidad;
            this.ExistenciaActual = ExistenciaActual;
            this.ExistenciaPosterior = ExistenciaPosterior;
            this.Concepto = Concepto;
            this.Fecha = Fecha;
        }

        public Movimiento(string CodigoProducto, int IdUsuario,
            ViewModels.EntradasSalidas.Tipo Tipo, float Cantidad, float ExistenciaActual, float ExistenciaPosterior,
            string Concepto, DateTime Fecha, string Nombre)
        {
            this.CodigoProducto = CodigoProducto;
            this.Nombre = Nombre;
            this.IdMovimiento = IdMovimiento;
            this.IdProducto = Producto.ObtenerId(CodigoProducto);
            this.IdUsuario = IdUsuario;
            this.Tipo = Tipo;
            this.Cantidad = Cantidad;
            this.ExistenciaActual = ExistenciaActual;
            this.ExistenciaPosterior = ExistenciaPosterior;
            this.Concepto = Concepto;
            this.Fecha = Fecha;
        }
        public static Movimiento Obtener(int ID_Movimiento)
        {
            Movimiento movimiento = null;
            using (IReader leector = Conexion.Sqlite.Read("SELECT * FROM MOVIMIENTOS WHERE ID =' " + ID_Movimiento + "' ;"))
            {
                if (leector.Read())
                {
                    int ID = Convert.ToInt32(leector["ID"]);
                    int ID_Producto = Convert.ToInt32(leector["ID_PRODUCTO"]);
                    int ID_Usuario = Convert.ToInt32(leector["ID_USUARIO"]);
                    char Tipo = Convert.ToChar(leector["TIPO"]);
                    float Cantidad = Convert.ToSingle(leector["CANTIDAD"]);
                    float Existencia_actual = Convert.ToSingle(leector["EXISTENCIA_ACTUAL"]);
                    float Existencia_posterior = Convert.ToSingle(leector["EXISTENCIA_POSTERIOR"]);
                    string Concepto = leector["CONCEPTO"].ToString();
                    DateTime Fecha = (DateTime)leector["FECHA"];

                    movimiento = new Movimiento(ID, ID_Producto, ID_Usuario, Tipo, Cantidad, Existencia_actual, Existencia_posterior, Concepto, Fecha);
                }
            }
            return movimiento;
        }
        public void RegistrarMovimiento()
        {
            using (var con = Conexion.Sqlite.Conecction())
            {
                Conexion.Sqlite.EXEC(con,
                    "INSERT INTO MOVIMIENTOS (ID_PRODUCTO,ID_USUARIO,TIPO,CANTIDAD,EXISTENCIA_ACTUAL,EXISTENCIA_POSTERIOR,CONCEPTO,FECHA) VALUES(?,?,?,?,?,?,?,?);"
                    , IdProducto, IdUsuario, Tipo == ViewModels.EntradasSalidas.Tipo.Entrada ? "E" : "S", Cantidad, ExistenciaActual, ExistenciaPosterior, Concepto, SQLHelper.FormatTime(Fecha));
                this.IdMovimiento = Conexion.Sqlite.LastScopeIdentity(con);
            }
            Conexion.Sqlite.EXEC("UPDATE PRODUCTOS SET EXISTENCIA=? WHERE ID=?", this.ExistenciaPosterior, this.IdProducto);
        }

    }

}
