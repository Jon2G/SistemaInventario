using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    public class Movimiento
    {
        public int ID_Movimiento { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Usuario { get; set; }
        /// <summary>
        /// Letra que representa el tipo de movimiento ('S'/'E') (Salida/Entrada)
        /// </summary>
        public char Tipo { get; set; }
        public float Cantidad { get; set; }
        public float Existencia_actual { get; set; }
        public float Existencia_posterior { get; set; }
        public string Concepto { get; set; }
        public string Fecha { get; set; }

        public Movimiento(int ID_Movimiento, int ID_Producto, int ID_Usuario, char Tipo, float Cantidad, float Existencia_actual, float Existencia_posterior, string Concepto, string Fecha)
        {
            this.ID_Movimiento = ID_Movimiento;
            this.ID_Producto = ID_Producto;
            this.ID_Usuario = ID_Usuario;
            this.Tipo = Tipo;
            this.Cantidad = Cantidad;
            this.Existencia_actual = Existencia_actual;
            this.Existencia_posterior = Existencia_posterior;
            this.Concepto = Concepto;
            this.Fecha = Fecha;
        }
        public static Movimiento Obtener(int ID_Movimiento)
        {
            Movimiento movimiento = null;
            using (IReader leector = Conexion.Sqlite.Leector("SELECT * FROM MOVIMIENTOS WHERE ID =' " + ID_Movimiento + "' ;"))
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
                    string Fecha = leector["FECHA"].ToString();

                    movimiento = new Movimiento(ID, ID_Producto, ID_Usuario, Tipo, Cantidad, Existencia_actual, Existencia_posterior, Concepto, Fecha);
                }
            }
            return movimiento;
        }
        public void RegistrarMovimiento()
        {
            Conexion.Sqlite.EXEC(
                "INSERT INTO MOVIMIENTOS (ID_PRODUCTO,ID_USUARIO,TIPO,CANTIDAD,EXISTENCIA_ACTUAL,EXISTENCIA_POSTERIOR,CONCEPTO,FECHA) VALUES(?,?,?,?,?,?,?,?);"
                , ID_Producto, ID_Usuario, Tipo, Cantidad, Existencia_actual, Existencia_posterior, Concepto, Fecha);
        }

    }

}
